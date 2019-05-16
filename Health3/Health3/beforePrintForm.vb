Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class beforePrintForm
    '事業所名
    Private ind As String
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
    ''' <param name="ind">事業所名</param>
    ''' <param name="nam">氏名</param>
    ''' <param name="kana">カナ</param>
    ''' <param name="sex">性別</param>
    ''' <param name="birth">生年月日</param>
    ''' <param name="printState">印刷状態</param>
    ''' <remarks></remarks>
    Public Sub New(ind As String, nam As String, kana As String, sex As String, birth As String, printState As Boolean)
        InitializeComponent()
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        Me.ind = ind
        Me.nam = nam
        Me.kana = kana
        Me.sex = sex
        Me.birth = birth
        Me.printState = printState
    End Sub

    Private Sub beforePrintForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    ''' <summary>
    ''' 基本項目印刷
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub printPaper()
        'エクセル準備
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("診断書２改")
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '受診日
        oSheet.Range("S3").Value = "受診日：令和　　　　年　　　　月　　　　日 (　　　　　　)"
        'カナ
        oSheet.Range("H5").Value = kana
        '氏名
        oSheet.Range("H6").Value = nam
        '性別
        oSheet.Range("L8").Value = If(sex = 1, "①　男　・　2　女", "1　男　・　②　女")
        '生年月日
        Dim wareki As String = birth
        Dim age As Integer = Util.calcAge(Util.convWarekiStrToADStr(birth), Today.ToString("yyyy/MM/dd"))
        oSheet.Range("H9").Value = wareki.Split("/")(0) & "　年　" & wareki.Split("/")(1) & "　月　" & wareki.Split("/")(2) & "　日"
        oSheet.Range("O9").Value = age & "　歳"
        '事業所名
        oSheet.Range("W5").Value = ind

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

    ''' <summary>
    ''' 実行ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExecute_Click(sender As System.Object, e As System.EventArgs) Handles btnExecute.Click
        If rbtnPrint.Checked Then
            '基本項目印刷
            printPaper()
            Me.Close()
        Else
            '健診結果入力フォーム呼び出し用返り値
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' キャンセルボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class