Imports System.Text

Public Class resultInputDataGridView
    Inherits DataGridView

    '性別
    Private _sex As String
    Public Property sex() As String
        Get
            Return _sex
        End Get
        Set(ByVal value As String)
            _sex = value
        End Set
    End Property

    '文字数制限用
    Private Const LIMIT_LENGTH_BYTE As Integer = 60

    '標準体重算出用BMI
    Private Const STANDARD_BMI As Double = 22

    Protected Overrides Function ProcessDialogKey(keyData As System.Windows.Forms.Keys) As Boolean
        Dim currentRowIndex As Integer = Me.CurrentCell.RowIndex
        Dim currentColumnIndex As Integer = Me.CurrentCell.ColumnIndex
        If keyData = Keys.Enter Then
            If currentColumnIndex = 1 Then '結果列に移動
                Me.CurrentCell = Me("Result", currentRowIndex)
                Me.BeginEdit(True)
            ElseIf currentColumnIndex = 4 Then
                Dim targetRowNumber() As Integer = {16, 21, 25, 34, 39, 40, 49, 62, 64, 66, 72, 75, 77, 78, 79, 81, 82, 84, 87}
                If Array.IndexOf(targetRowNumber, currentRowIndex) >= 0 Then
                    Me.CurrentCell = Me("Kubun", currentRowIndex + 1)
                    Me.BeginEdit(True)
                ElseIf currentRowIndex = 85 Then
                    Me.CurrentCell = Me("Kubun", 87)
                    Me.BeginEdit(True)
                End If
            End If
            Return False
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    Protected Overrides Function ProcessDataGridViewKey(e As System.Windows.Forms.KeyEventArgs) As Boolean
        Dim tb As DataGridViewTextBoxEditingControl = CType(Me.EditingControl, DataGridViewTextBoxEditingControl)
        If Not IsNothing(tb) AndAlso ((e.KeyCode = Keys.Left AndAlso tb.SelectionStart = 0) OrElse (e.KeyCode = Keys.Right AndAlso tb.SelectionStart = tb.TextLength)) Then
            Return False
        Else
            Return MyBase.ProcessDataGridViewKey(e)
        End If
    End Function

    ''' <summary>
    ''' 検査結果列入力時の指導区分等算出処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub resultInputDataGridView_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellEndEdit
        Dim currentCellRowIndex As Integer = Me.CurrentCell.RowIndex
        Dim currentCellColumnIndex As Integer = Me.CurrentCell.ColumnIndex

        '指導区分の算出等
        If currentCellColumnIndex = 4 AndAlso (currentCellRowIndex = 0 OrElse currentCellRowIndex = 1) Then
            '身長 or 体重を入力後、標準体重とbmi算出、BMIの値で指導区分を算出
            Dim heightStr As String = Util.checkDBNullValue(Me("Result", 0).Value)
            Dim weightStr As String = Util.checkDBNullValue(Me("Result", 1).Value)
            If (heightStr <> "0" AndAlso weightStr <> "0") AndAlso System.Text.RegularExpressions.Regex.IsMatch(heightStr, "^\d+(\.\d+)?$") AndAlso System.Text.RegularExpressions.Regex.IsMatch(weightStr, "^\d+(\.\d+)?$") Then
                Dim height As Double = heightStr
                Dim weight As Double = weightStr
                Dim bmi As Double = Math.Round(weight / ((height / 100) * (height / 100)), 1, MidpointRounding.AwayFromZero)
                Dim standardWeight As Double = Math.Round(STANDARD_BMI * (height / 100) * (height / 100), 1, MidpointRounding.AwayFromZero)
                Me("Result", 3).Value = bmi.ToString("#.0")
                Me("Result", 2).Value = standardWeight.ToString("#.0")
                '指導区分
                If 18.5 <= bmi AndAlso bmi < 25.0 Then
                    Me("Kubun", 0).Value = "1"
                Else
                    Me("Kubun", 0).Value = "3"
                End If
            Else
                'BMI
                Me("Result", 3).Value = ""
                '標準体重
                Me("Result", 2).Value = ""
                '指導区分
                Me("Kubun", 0).Value = ""
            End If
        ElseIf currentCellColumnIndex = 4 AndAlso (currentCellRowIndex = 17 OrElse currentCellRowIndex = 19) Then
            '最高血圧 or 最低血圧入力後、指導区分を算出
            Dim highStr As String = Util.checkDBNullValue(Me("Result", 17).Value)
            Dim lowStr As String = Util.checkDBNullValue(Me("Result", 19).Value)
            If System.Text.RegularExpressions.Regex.IsMatch(highStr, "^\d+$") AndAlso System.Text.RegularExpressions.Regex.IsMatch(lowStr, "^\d+$") Then
                Dim high As Integer = CInt(highStr)
                Dim low As Integer = CInt(lowStr)

                '最高血圧区分
                Dim highKubun As Integer
                If 90 <= high AndAlso high <= 139 Then
                    highKubun = 1
                ElseIf high <= 89 OrElse (140 <= high AndAlso high <= 149) Then
                    highKubun = 2
                ElseIf 150 <= high AndAlso high <= 159 Then
                    highKubun = 3
                ElseIf 160 <= high Then
                    highKubun = 4
                End If
                '最低血圧区分
                Dim lowKubun As Integer
                If low <= 89 Then
                    lowKubun = 1
                ElseIf 90 <= low AndAlso low <= 94 Then
                    lowKubun = 2
                ElseIf 95 <= low AndAlso low <= 99 Then
                    lowKubun = 3
                ElseIf 100 <= low Then
                    lowKubun = 4
                End If

                '指導区分
                Me("Kubun", 17).Value = If(highKubun <= lowKubun, lowKubun, highKubun)
            Else
                Me("Kubun", 17).Value = ""
            End If

            '入力時にメタボ判定
            decisionMetabo()
        ElseIf currentCellColumnIndex = 4 AndAlso (currentCellRowIndex = 22 OrElse currentCellRowIndex = 23 OrElse currentCellRowIndex = 24) Then
            '脂質
            Dim kubun As Integer = 0
            Dim sokoreStr As String = Util.checkDBNullValue(Me("Result", 22).Value)
            Dim tyuseiStr As String = Util.checkDBNullValue(Me("Result", 23).Value)
            Dim hdlStr As String = Util.checkDBNullValue(Me("Result", 24).Value)
            '総ｺﾚｽﾃﾛｰﾙ
            If System.Text.RegularExpressions.Regex.IsMatch(sokoreStr, "^\d+$") Then
                Dim sKubun As Integer
                Dim sokore As Integer = CInt(sokoreStr)
                If 140 <= sokore AndAlso sokore <= 199 Then
                    sKubun = 1
                ElseIf 200 <= sokore AndAlso sokore <= 219 Then
                    sKubun = 2
                ElseIf 220 <= sokore AndAlso sokore <= 239 Then
                    sKubun = 3
                ElseIf 240 <= sokore Then
                    sKubun = 4
                ElseIf sokore <= 139 Then
                    sKubun = 5
                End If
                kubun = sKubun
            End If
            '中性脂肪
            If System.Text.RegularExpressions.Regex.IsMatch(tyuseiStr, "^\d+$") Then
                Dim tKubun As Integer
                Dim tyusei As Integer = CInt(tyuseiStr)
                If tyusei <= 149 Then
                    tKubun = 1
                ElseIf 150 <= tyusei AndAlso tyusei <= 199 Then
                    tKubun = 2
                ElseIf 200 <= tyusei AndAlso tyusei <= 249 Then
                    tKubun = 3
                ElseIf 250 <= tyusei Then
                    tKubun = 4
                End If
                kubun = If(kubun <= tKubun, tKubun, kubun)
            End If
            'HDL
            If System.Text.RegularExpressions.Regex.IsMatch(hdlStr, "^\d+$") Then
                Dim hKubun As Integer
                Dim hdl As Integer = CInt(hdlStr)
                If 40 <= hdl Then
                    hKubun = 1
                ElseIf 35 <= hdl AndAlso hdl <= 39 Then
                    hKubun = 3
                ElseIf hdl <= 34 Then
                    hKubun = 4
                End If
                kubun = If(kubun <= hKubun, hKubun, kubun)
            End If

            '中性脂肪、HDL入力時にメタボ判定
            If currentCellRowIndex = 23 OrElse currentCellRowIndex = 24 Then
                decisionMetabo()
            End If

            '指導区分
            Me("Kubun", 22).Value = If(kubun = 0, "", kubun)
        ElseIf currentCellColumnIndex = 4 AndAlso (26 <= currentCellRowIndex AndAlso currentCellRowIndex <= 34) Then
            '肝機能等
            Dim kubun As Integer = 0
            Dim gotStr As String = Util.checkDBNullValue(Me("Result", 26).Value)
            Dim gptStr As String = Util.checkDBNullValue(Me("Result", 27).Value)
            Dim rStr As String = Util.checkDBNullValue(Me("Result", 28).Value)
            Dim alpStr As String = Util.checkDBNullValue(Me("Result", 29).Value)
            Dim sotanStr As String = Util.checkDBNullValue(Me("Result", 30).Value)
            Dim albStr As String = Util.checkDBNullValue(Me("Result", 31).Value)
            Dim sobiriStr As String = Util.checkDBNullValue(Me("Result", 32).Value)
            Dim ldhStr As String = Util.checkDBNullValue(Me("Result", 33).Value)
            Dim amiStr As String = Util.checkDBNullValue(Me("Result", 34).Value)

            'ＧＯＴ
            If System.Text.RegularExpressions.Regex.IsMatch(gotStr, "^\d+$") Then
                Dim gotKubun As Integer
                Dim got As Integer = CInt(gotStr)
                If got <= 35 Then
                    gotKubun = 1
                ElseIf 36 <= got AndAlso got <= 49 Then
                    gotKubun = 3
                ElseIf 50 <= got Then
                    gotKubun = 5
                End If
                kubun = If(kubun <= gotKubun, gotKubun, kubun)
            End If
            'ＧＰＴ
            If System.Text.RegularExpressions.Regex.IsMatch(gptStr, "^\d+$") Then
                Dim gptKubun As Integer
                Dim gpt As Integer = CInt(gptStr)
                If gpt <= 35 Then
                    gptKubun = 1
                ElseIf 36 <= gpt AndAlso gpt <= 49 Then
                    gptKubun = 3
                ElseIf 50 <= gpt Then
                    gptKubun = 5
                End If
                kubun = If(kubun <= gptKubun, gptKubun, kubun)
            End If
            'γーＧＴＰ
            If System.Text.RegularExpressions.Regex.IsMatch(rStr, "^\d+$") Then
                Dim rKubun As Integer
                Dim r As Integer = CInt(rStr)
                If r <= 55 Then
                    rKubun = 1
                ElseIf 56 <= r AndAlso r <= 79 Then
                    rKubun = 2
                ElseIf 80 <= r AndAlso r <= 99 Then
                    rKubun = 3
                ElseIf 100 <= r Then
                    rKubun = 5
                End If
                kubun = If(kubun <= rKubun, rKubun, kubun)
            End If
            'ＡＬＰ
            If System.Text.RegularExpressions.Regex.IsMatch(alpStr, "^\d+$") Then
                Dim alpKubun As Integer
                Dim alp As Integer = CInt(alpStr)
                If alp <= 339 Then
                    alpKubun = 1
                ElseIf 340 <= alp AndAlso alp <= 449 Then
                    alpKubun = 3
                ElseIf 450 <= alp Then
                    alpKubun = 5
                End If
                kubun = If(kubun <= alpKubun, alpKubun, kubun)
            End If
            '総蛋白
            If System.Text.RegularExpressions.Regex.IsMatch(sotanStr, "^\d+(\.\d+)?$") Then
                Dim sotanKubun As Integer
                Dim sotan As Double = CDbl(sotanStr)
                If 6.5 <= sotan AndAlso sotan <= 8 Then
                    sotanKubun = 1
                ElseIf 8.1 <= sotan AndAlso sotan <= 9 Then
                    sotanKubun = 2
                ElseIf 6.0 <= sotan AndAlso sotan <= 6.4 Then
                    sotanKubun = 3
                Else
                    sotanKubun = 5
                End If
                kubun = If(kubun <= sotanKubun, sotanKubun, kubun)
            End If
            'ｱﾙﾌﾞﾐﾝ
            If System.Text.RegularExpressions.Regex.IsMatch(albStr, "^\d+(\.\d+)?$") Then
                Dim albKubun As Integer
                Dim alb As Double = CDbl(albStr)
                If 4 <= alb Then
                    albKubun = 1
                Else
                    albKubun = 3
                End If
                kubun = If(kubun <= albKubun, albKubun, kubun)
            End If
            '総ﾋﾞﾘﾙﾋﾞﾝ
            If System.Text.RegularExpressions.Regex.IsMatch(sobiriStr, "^\d+(\.\d+)?$") Then
                Dim sobiriKubun As Integer
                Dim sobiri As Double = CDbl(sobiriStr)
                If sobiri < 1.2 Then
                    sobiriKubun = 1
                ElseIf 1.2 <= sobiri AndAlso sobiri <= 1.9 Then
                    sobiriKubun = 3
                ElseIf 2 <= sobiri Then
                    sobiriKubun = 5
                End If
                kubun = If(kubun <= sobiriKubun, sobiriKubun, kubun)
            End If
            'ＬＤＨ
            If System.Text.RegularExpressions.Regex.IsMatch(ldhStr, "^\d+$") Then
                Dim ldhKubun As Integer
                Dim ldh As Integer = CInt(ldhStr)
                If ldh <= 229 Then
                    ldhKubun = 1
                ElseIf 230 <= ldh AndAlso ldh <= 349 Then
                    ldhKubun = 3
                ElseIf 350 <= ldh Then
                    ldhKubun = 5
                End If
                kubun = If(kubun <= ldhKubun, ldhKubun, kubun)
            End If
            'ｱﾐﾗｰｾﾞ
            If System.Text.RegularExpressions.Regex.IsMatch(amiStr, "^\d+$") Then
                Dim amiKubun As Integer
                Dim ami As Integer = CInt(amiStr)
                If 50 <= ami AndAlso ami <= 200 Then
                    amiKubun = 1
                ElseIf 201 <= ami AndAlso ami <= 250 Then
                    amiKubun = 3
                Else
                    amiKubun = 5
                End If
                kubun = If(kubun <= amiKubun, amiKubun, kubun)
            End If
            '指導区分
            Me("Kubun", 26).Value = If(kubun = 0, "", kubun)
        ElseIf currentCellColumnIndex = 4 AndAlso (currentCellRowIndex = 35 OrElse currentCellRowIndex = 37) Then
            '血糖
            Dim kubun As Integer = 0
            Dim ketoStr As String = Util.checkDBNullValue(Me("Result", 35).Value)
            Dim touStr As String = Util.checkDBNullValue(Me("Result", 37).Value)

            '血糖
            If System.Text.RegularExpressions.Regex.IsMatch(ketoStr, "^\d+$") Then
                Dim kKubun As Integer
                Dim keto As Integer = CInt(ketoStr)
                If keto <= 109 Then
                    kKubun = 1
                ElseIf 110 <= keto AndAlso keto <= 115 Then
                    kKubun = 3
                ElseIf 116 <= keto Then
                    kKubun = 5
                End If
                kubun = If(kubun <= kKubun, kKubun, kubun)
            End If
            '尿糖
            If System.Text.RegularExpressions.Regex.IsMatch(touStr, "[1-5]") Then
                Dim tKubun As Integer
                Dim tou As Integer = CInt(touStr)
                If tou = 1 Then
                    tKubun = 1
                ElseIf tou = 2 Then
                    tKubun = 2
                ElseIf tou = 3 Then
                    tKubun = 3
                ElseIf tou = 4 OrElse tou = 5 Then
                    tKubun = 5
                End If
                kubun = If(kubun <= tKubun, tKubun, kubun)
            End If

            '血糖入力時にメタボ判定
            If currentCellRowIndex = 35 Then
                decisionMetabo()
            End If

            '指導区分
            Me("Kubun", 35).Value = If(kubun = 0, "", kubun)
        ElseIf currentCellColumnIndex = 4 AndAlso currentCellRowIndex = 40 Then
            '尿酸
            Dim kubun As Integer = 0
            Dim nyosanStr As String = Util.checkDBNullValue(Me("Result", 40).Value)
            If System.Text.RegularExpressions.Regex.IsMatch(nyosanStr, "^\d+(\.\d+)?$") Then
                Dim nKubun As Integer
                Dim nyosan As Double = CDbl(nyosanStr)
                If nyosan <= 7 Then
                    nKubun = 1
                ElseIf 7.1 <= nyosan AndAlso nyosan <= 7.9 Then
                    nKubun = 3
                ElseIf 8 <= nyosan Then
                    nKubun = 4
                End If
                kubun = If(kubun <= nKubun, nKubun, kubun)
            End If
            '指導区分
            Me("Kubun", 40).Value = If(kubun = 0, "", kubun)
        ElseIf currentCellColumnIndex = 4 AndAlso (41 <= currentCellRowIndex AndAlso currentCellRowIndex <= 44) Then
            '尿一般・腎
            Dim kubun As Integer = 0
            Dim nyotanStr As String = Util.checkDBNullValue(Me("Result", 41).Value)
            Dim nyosenStr As String = Util.checkDBNullValue(Me("Result", 43).Value)
            Dim kureStr As String = Util.checkDBNullValue(Me("Result", 44).Value)
            '尿蛋白
            If System.Text.RegularExpressions.Regex.IsMatch(nyotanStr, "[1-5]") Then
                Dim tKubun As Integer
                Dim nyotan As Integer = CInt(nyotanStr)
                If nyotan = 1 Then
                    tKubun = 1
                ElseIf nyotan = 2 Then
                    tKubun = 2
                ElseIf nyotan = 3 Then
                    tKubun = 3
                ElseIf nyotan = 4 OrElse nyotan = 5 Then
                    tKubun = 5
                End If
                kubun = If(kubun <= tKubun, tKubun, kubun)
            End If
            '尿潜血
            If System.Text.RegularExpressions.Regex.IsMatch(nyosenStr, "[1-5]") Then
                Dim sKubun As Integer
                Dim nyosen As Integer = CInt(nyosenStr)
                If nyosen = 1 Then
                    sKubun = 1
                ElseIf nyosen = 2 Then
                    sKubun = 2
                ElseIf nyosen = 3 Then
                    sKubun = 3
                ElseIf nyosen = 4 OrElse nyosen = 5 Then
                    sKubun = 5
                End If
                kubun = If(kubun <= sKubun, sKubun, kubun)
            End If
            '血清ｸﾚｱﾁﾆﾝ
            If System.Text.RegularExpressions.Regex.IsMatch(kureStr, "^\d+(\.\d+)?$") Then
                Dim kKubun As Integer
                Dim kure As Double = CDbl(kureStr)
                If sex = "1" Then
                    '男性
                    If kure < 1.2 Then
                        kKubun = 1
                    ElseIf 1.2 <= kure AndAlso kure < 1.4 Then
                        kKubun = 3
                    ElseIf 1.4 <= kure Then
                        kKubun = 5
                    End If
                ElseIf sex = "2" Then
                    '女性
                    If kure <= 0.8 Then
                        kKubun = 1
                    ElseIf 0.8 < kure AndAlso kure <= 1 Then
                        kKubun = 3
                    ElseIf 1 < kure Then
                        kKubun = 5
                    End If
                End If
                kubun = If(kubun <= kKubun, kKubun, kubun)
            End If
            '指導区分
            Me("Kubun", 41).Value = If(kubun = 0, "", kubun)
        ElseIf currentCellColumnIndex = 4 AndAlso (50 <= currentCellRowIndex AndAlso currentCellRowIndex <= 54) Then
            '血液一般
            Dim kubun As Integer = 0
            Dim hemaStr As String = Util.checkDBNullValue(Me("Result", 50).Value)
            Dim hemoStr As String = Util.checkDBNullValue(Me("Result", 51).Value)
            Dim sekeStr As String = Util.checkDBNullValue(Me("Result", 52).Value)
            Dim hakeStr As String = Util.checkDBNullValue(Me("Result", 53).Value)
            Dim kesyoStr As String = Util.checkDBNullValue(Me("Result", 54).Value)

            'ﾍﾏﾄｸﾘｯﾄ
            If System.Text.RegularExpressions.Regex.IsMatch(hemaStr, "^\d+(\.\d+)?$") Then
                Dim hKubun As Integer
                Dim hema As Double = CDbl(hemaStr)
                If sex = "1" Then
                    '男性
                    If 39 <= hema AndAlso hema < 49 Then
                        hKubun = 1
                    ElseIf 49 <= hema AndAlso hema < 52 Then
                        hKubun = 2
                    ElseIf 35 <= hema AndAlso hema < 39 Then
                        hKubun = 3
                    ElseIf hema < 35 OrElse 52 <= hema Then
                        hKubun = 4
                    End If
                ElseIf sex = "2" Then
                    '女性
                    If 34 <= hema AndAlso hema < 44 Then
                        hKubun = 1
                    ElseIf 44 <= hema AndAlso hema < 46 Then
                        hKubun = 2
                    ElseIf 31 <= hema AndAlso hema < 34 Then
                        hKubun = 3
                    ElseIf hema < 31 OrElse 46 <= hema Then
                        hKubun = 4
                    End If
                End If
                kubun = If(kubun <= hKubun, hKubun, kubun)
            End If
            'ﾍﾓｸﾞﾛﾋﾞﾝ
            If System.Text.RegularExpressions.Regex.IsMatch(hemoStr, "^\d+(\.\d+)?$") Then
                Dim hKubun As Integer
                Dim hemo As Double = CDbl(hemoStr)
                If sex = "1" Then
                    '男性
                    If 13 <= hemo AndAlso hemo < 16.7 Then
                        hKubun = 1
                    ElseIf 16.7 <= hemo AndAlso hemo < 17.6 Then
                        hKubun = 2
                    ElseIf 12 <= hemo AndAlso hemo < 13 Then
                        hKubun = 3
                    ElseIf hemo < 12 OrElse 17.6 <= hemo Then
                        hKubun = 4
                    End If
                ElseIf sex = "2" Then
                    '女性
                    If 11.4 <= hemo AndAlso hemo < 14.7 Then
                        hKubun = 1
                    ElseIf 14.7 <= hemo AndAlso hemo < 15.5 Then
                        hKubun = 2
                    ElseIf 10.8 <= hemo AndAlso hemo < 11.4 Then
                        hKubun = 3
                    ElseIf hemo < 10.8 OrElse 15.5 <= hemo Then
                        hKubun = 4
                    End If
                End If
                kubun = If(kubun <= hKubun, hKubun, kubun)
            End If
            '赤血球数
            If System.Text.RegularExpressions.Regex.IsMatch(sekeStr, "^\d+$") Then
                Dim sKubun As Integer
                Dim seke As Integer = CInt(sekeStr)
                If sex = "1" Then
                    '男性
                    If 400 <= seke AndAlso seke < 540 Then
                        sKubun = 1
                    ElseIf 540 <= seke AndAlso seke < 580 Then
                        sKubun = 2
                    ElseIf 360 <= seke AndAlso seke < 400 Then
                        sKubun = 3
                    ElseIf seke < 360 OrElse 580 <= seke Then
                        sKubun = 4
                    End If
                ElseIf sex = "2" Then
                    '女性
                    If 360 <= seke AndAlso seke < 490 Then
                        sKubun = 1
                    ElseIf 490 <= seke AndAlso seke < 520 Then
                        sKubun = 2
                    ElseIf 330 <= seke AndAlso seke < 360 Then
                        sKubun = 3
                    ElseIf seke < 330 OrElse 520 <= seke Then
                        sKubun = 4
                    End If
                End If
                kubun = If(kubun <= sKubun, sKubun, kubun)
            End If
            '白血球数
            If System.Text.RegularExpressions.Regex.IsMatch(hakeStr, "^\d+(\.\d+)?$") Then
                Dim hKubun As Integer
                Dim hake As Double = CDbl(hakeStr)
                If 33 <= hake AndAlso hake < 90 Then
                    hKubun = 1
                ElseIf 90 <= hake AndAlso hake < 110 Then
                    hKubun = 2
                ElseIf 26 <= hake AndAlso hake < 33 Then
                    hKubun = 3
                ElseIf hake < 26 OrElse 110 <= hake Then
                    hKubun = 4
                End If
                kubun = If(kubun <= hKubun, hKubun, kubun)
            End If
            '血小板数
            If System.Text.RegularExpressions.Regex.IsMatch(kesyoStr, "^\d+(\.\d+)?$") Then
                Dim kKubun As Integer
                Dim kesyo As Double = CDbl(kesyoStr)
                If 14 <= kesyo AndAlso kesyo < 36 Then
                    kKubun = 1
                ElseIf 36 <= kesyo AndAlso kesyo < 45 Then
                    kKubun = 2
                ElseIf 11 <= kesyo AndAlso kesyo < 14 Then
                    kKubun = 3
                ElseIf kesyo < 11 OrElse 45 <= kesyo Then
                    kKubun = 4
                End If
                kubun = If(kubun <= kKubun, kKubun, kubun)
            End If
            '指導区分
            Me("Kubun", 50).Value = If(kubun = 0, "", kubun)
        ElseIf currentCellColumnIndex = 4 AndAlso (80 <= currentCellRowIndex AndAlso currentCellRowIndex <= 81) Then
            '大腸
            Dim kubun As Integer = 0
            Dim ben1Str As String = Util.checkDBNullValue(Me("Result", 80).Value)
            Dim ben2Str As String = Util.checkDBNullValue(Me("Result", 81).Value)
            '便1日目
            If System.Text.RegularExpressions.Regex.IsMatch(ben1Str, "[13]") Then
                Dim bKubun As Integer
                Dim ben1 As Integer = CInt(ben1Str)
                If ben1 = 1 Then
                    bKubun = 1
                ElseIf ben1 = 3 Then
                    bKubun = 5
                End If
                kubun = If(kubun <= bKubun, bKubun, kubun)
            End If
            '便2日目
            If System.Text.RegularExpressions.Regex.IsMatch(ben2Str, "[13]") Then
                Dim bKubun As Integer
                Dim ben2 As Integer = CInt(ben2Str)
                If ben2 = 1 Then
                    bKubun = 1
                ElseIf ben2 = 3 Then
                    bKubun = 5
                End If
                kubun = If(kubun <= bKubun, bKubun, kubun)
            End If
            '指導区分
            Me("Kubun", 80).Value = If(kubun = 0, "", kubun)
        ElseIf currentCellColumnIndex = 4 AndAlso (currentCellRowIndex = 4) Then
            '腹囲の入力時にメタボ判定
            decisionMetabo()
        End If

    End Sub

    ''' <summary>
    ''' ﾒﾀﾎﾞﾘｯｸｼﾝﾄﾞﾛｰﾑ判定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub decisionMetabo()
        '腹囲未入力　　　　　　　　　 ：4
        '腹囲条件非該当　　　　　　　 ：3
        '腹囲条件のみ該当　　　　　　 ：2
        '腹囲条件該当且つ他項目1つ該当：2
        '腹囲条件該当且つ他項目2つ該当：1
        Dim metabo As Integer

        '腹囲条件判定
        Dim hukuiStr As String = Util.checkDBNullValue(Me("Result", 4).Value)
        If System.Text.RegularExpressions.Regex.IsMatch(hukuiStr, "^\d+(\.\d+)?$") Then
            Dim hukui As Double = CDbl(hukuiStr)
            If (sex = "1" AndAlso 85 <= hukui) OrElse (sex = "2" AndAlso 90 <= hukui) Then
                metabo = 2
                Me("Result", 100).Value = metabo
            Else
                metabo = 3
                Me("Result", 100).Value = metabo
            End If
        Else
            metabo = 4
            Me("Result", 100).Value = metabo
        End If

        '他項目条件該当数
        Dim count As Integer = 0

        '中性脂肪
        Dim tyuseiStr As String = Util.checkDBNullValue(Me("Result", 23).Value)
        If System.Text.RegularExpressions.Regex.IsMatch(tyuseiStr, "^\d+$") Then
            Dim tyusei As Integer = CInt(tyuseiStr)
            If 150 <= tyusei Then
                count += 1
            End If
        End If
        'ＨＤＬ
        Dim hdlStr As String = Util.checkDBNullValue(Me("Result", 24).Value)
        If System.Text.RegularExpressions.Regex.IsMatch(hdlStr, "^\d+$") Then
            Dim hdl As Integer = CInt(hdlStr)
            If hdl <= 40 Then
                count += 1
            End If
        End If
        '血圧　最高、最低
        Dim highStr As String = Util.checkDBNullValue(Me("Result", 17).Value)
        Dim lowStr As String = Util.checkDBNullValue(Me("Result", 19).Value)
        If System.Text.RegularExpressions.Regex.IsMatch(highStr, "^\d+$") AndAlso System.Text.RegularExpressions.Regex.IsMatch(lowStr, "^\d+$") Then
            Dim high As Integer = CInt(highStr)
            Dim low As Integer = CInt(lowStr)
            If 130 <= high AndAlso 85 <= low Then
                count += 1
            End If
        End If
        '血糖
        Dim ketoStr As String = Util.checkDBNullValue(Me("Result", 35).Value)
        If System.Text.RegularExpressions.Regex.IsMatch(ketoStr, "^\d+$") Then
            Dim keto As Integer = CInt(ketoStr)
            If 110 <= keto Then
                count += 1
            End If
        End If

        '腹囲条件該当且つ他項目2つ以上該当
        If metabo = 2 AndAlso 2 <= count Then
            metabo = 1
            Me("Result", 100).Value = metabo
        End If
    End Sub

    Private Sub resultInputDataGridView_CellPainting(sender As Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles Me.CellPainting
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            e.Graphics.FillRectangle(New SolidBrush(e.CellStyle.BackColor), e.CellBounds)

            Dim pParts As DataGridViewPaintParts
            If 0 <= e.ColumnIndex AndAlso e.ColumnIndex <= 3 Then
                pParts = e.PaintParts And Not DataGridViewPaintParts.Border
            ElseIf e.ColumnIndex = 4 Then
                If e.RowIndex = 2 OrElse e.RowIndex = 3 OrElse e.RowIndex = 89 OrElse e.RowIndex = 90 Then
                    pParts = e.PaintParts And Not DataGridViewPaintParts.Border
                Else
                    pParts = e.PaintParts
                End If
            End If

            '縦線
            If e.ColumnIndex = 0 OrElse e.ColumnIndex = 1 OrElse e.ColumnIndex = 2 Then
                With e.CellBounds
                    .Offset(-1, 0)
                    e.Graphics.DrawLine(New Pen(Color.FromKnownColor(KnownColor.ControlDark)), .Right, .Top, .Right, .Bottom)
                End With
            End If

            '横線(全部の列)
            '灰色線
            If e.RowIndex = 17 OrElse e.RowIndex = 22 OrElse e.RowIndex = 26 OrElse e.RowIndex = 35 OrElse e.RowIndex = 40 OrElse e.RowIndex = 41 OrElse e.RowIndex = 50 OrElse e.RowIndex = 63 OrElse e.RowIndex = 65 OrElse e.RowIndex = 67 OrElse e.RowIndex = 73 OrElse e.RowIndex = 76 OrElse e.RowIndex = 79 OrElse e.RowIndex = 80 OrElse e.RowIndex = 83 OrElse e.RowIndex = 85 OrElse e.RowIndex = 87 OrElse e.RowIndex = 93 OrElse e.RowIndex = 94 OrElse e.RowIndex = 100 Then
                With e.CellBounds
                    .Offset(0, -1)
                    e.Graphics.DrawLine(New Pen(Color.FromKnownColor(KnownColor.ControlDark)), .Left, .Top, .Right, .Top)
                End With
            End If
            '青線
            If e.RowIndex = 101 Then
                With e.CellBounds
                    .Offset(0, -1)
                    e.Graphics.DrawLine(New Pen(Color.Blue), .Left, .Top, .Right, .Top)
                End With
            End If

            '横線(3列目～)
            If e.ColumnIndex >= 2 AndAlso (e.RowIndex = 4 OrElse e.RowIndex = 6 OrElse e.RowIndex = 7 OrElse e.RowIndex = 8 OrElse e.RowIndex = 9 OrElse e.RowIndex = 13 OrElse e.RowIndex = 19 OrElse e.RowIndex = 21 OrElse e.RowIndex = 23 OrElse e.RowIndex = 24 OrElse e.RowIndex = 25 OrElse e.RowIndex = 27 OrElse e.RowIndex = 28 OrElse e.RowIndex = 29 OrElse e.RowIndex = 30 OrElse e.RowIndex = 36 OrElse e.RowIndex = 37 OrElse e.RowIndex = 39 OrElse e.RowIndex = 43 OrElse e.RowIndex = 44 OrElse e.RowIndex = 45 OrElse e.RowIndex = 51 OrElse e.RowIndex = 52 OrElse e.RowIndex = 53 OrElse e.RowIndex = 54 OrElse e.RowIndex = 82 OrElse e.RowIndex = 88 OrElse e.RowIndex = 91) Then
                With e.CellBounds
                    .Offset(0, -1)
                    e.Graphics.DrawLine(New Pen(Color.FromKnownColor(KnownColor.ControlDark)), .Left, .Top, .Right, .Top)
                End With
            End If
            'とりあえず
            If e.ColumnIndex = 1 AndAlso e.RowIndex = 88 Then
                With e.CellBounds
                    .Offset(0, -1)
                    e.Graphics.DrawLine(New Pen(Color.FromKnownColor(KnownColor.ControlDark)), .Left, .Top, .Right, .Top)
                End With
            End If

            e.Paint(e.ClipBounds, pParts)
            e.Handled = True
        End If
    End Sub

    Private Sub resultInputDataGridView_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
            Dim dgv As DataGridView = DirectCast(sender, DataGridView)

            '選択行index
            Dim selectedRowIndex As Integer = dgv.CurrentCell.RowIndex
            '選択列index
            Dim selectedColumnIndex As Integer = dgv.CurrentCell.ColumnIndex

            '編集のために表示されているテキストボックス取得、設定
            Dim tb As DataGridViewTextBoxEditingControl = DirectCast(e.Control, DataGridViewTextBoxEditingControl)
            tb.ImeMode = Windows.Forms.ImeMode.Alpha
            If selectedColumnIndex = 4 AndAlso (selectedRowIndex = 6 OrElse selectedRowIndex = 7 OrElse selectedRowIndex = 8 OrElse selectedRowIndex = 62 OrElse selectedRowIndex = 63 OrElse selectedRowIndex = 64 OrElse selectedRowIndex = 66 OrElse selectedRowIndex = 71 OrElse selectedRowIndex = 72 OrElse selectedRowIndex = 77 OrElse selectedRowIndex = 78 OrElse selectedRowIndex = 79 OrElse selectedRowIndex = 82 OrElse selectedRowIndex = 83 OrElse selectedRowIndex = 84 OrElse selectedRowIndex = 94 OrElse selectedRowIndex = 95 OrElse selectedRowIndex = 96 OrElse selectedRowIndex = 97 OrElse selectedRowIndex = 98 OrElse selectedRowIndex = 99 OrElse selectedRowIndex = 102 OrElse selectedRowIndex = 103 OrElse selectedRowIndex = 105 OrElse selectedRowIndex = 106 OrElse selectedRowIndex = 108 OrElse selectedRowIndex = 109) Then
                tb.ImeMode = Windows.Forms.ImeMode.Hiragana
            End If

            'イベントハンドラを削除
            RemoveHandler tb.KeyPress, AddressOf dgvTextBox_KeyPress

            If 94 <= selectedRowIndex AndAlso selectedRowIndex <= 99 Then
                '総合判定入力テキストボックス用
                AddHandler tb.KeyPress, AddressOf dgvTextBox_KeyPress
            End If
        End If
    End Sub

    ''' <summary>
    ''' 総合判定入力テキストボックス用KeyPressイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvTextBox_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        Dim text As String = CType(sender, DataGridViewTextBoxEditingControl).Text
        Dim lengthByte As Integer = Encoding.GetEncoding("Shift_JIS").GetByteCount(text)

        If lengthByte >= LIMIT_LENGTH_BYTE Then '設定されているバイト数以上の時
            If e.KeyChar = ChrW(Keys.Back) Then
                'Backspaceは入力可能
                e.Handled = False
            Else
                '入力できなくする
                e.Handled = True
            End If
        End If
    End Sub
End Class
