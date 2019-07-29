Public Class TopForm
    'データベースのパス
    Public dbFilePath As String = My.Application.Info.DirectoryPath & "\Health3.mdb"
    Public DB_Health3 As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbFilePath

    'エクセルのパス
    Public excelFilePass As String = My.Application.Info.DirectoryPath & "\Health3.xls"

    '.iniファイルのパス
    Public iniFilePath As String = My.Application.Info.DirectoryPath & "\Health3.ini"

    '画像パス
    Public imageFilePath As String = My.Application.Info.DirectoryPath & "\Health3.PNG"
    Public health3aPath As String = My.Application.Info.DirectoryPath & "\Health3a.PNG"
    Public health3bPath As String = My.Application.Info.DirectoryPath & "\Health3b.PNG"

    'Diagnoseのデータベースパス
    Public dbDiagnoseFilePath As String = Util.getIniString("System", "DiagnoseDir", iniFilePath) & "\Diagnose.mdb"
    Public DB_Diagnose As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbDiagnoseFilePath

    'SealBoxフォルダパス
    Public sealBoxDirPath As String = Util.getIniString("System", "SealBoxDir", iniFilePath)

    '各フォーム
    Dim examineeMasterForm As 受診者マスタ
    Dim officeMasterForm As 事業所マスタ
    Dim resultFDForm As 健診結果ＦＤ
    Dim examineeListForm As 受診者一覧
    Dim implementationHistoryForm As 事業所別実施履歴
    Dim resultReportForm As 健診結果報告書

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TopForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'データベース、エクセル、構成ファイルの存在チェック
        If Not System.IO.File.Exists(dbFilePath) Then
            MsgBox("Health3データベースファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If
        If Not System.IO.File.Exists(dbDiagnoseFilePath) Then
            MsgBox("Diagnoseデータベースファイルが存在しません。" & Environment.NewLine & "iniファイルのDiagnoseDirに適切なパスを設定して下さい。")
            Me.Close()
            Exit Sub
        End If

        If Not System.IO.File.Exists(excelFilePass) Then
            MsgBox("エクセルファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        If Not System.IO.File.Exists(iniFilePath) Then
            MsgBox("構成ファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        If Not System.IO.File.Exists(imageFilePath) Then
            MsgBox("画像ファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        If Not System.IO.File.Exists(health3aPath) Then
            MsgBox("胸部画像ファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        If Not System.IO.File.Exists(health3bPath) Then
            MsgBox("胃部画像ファイルが存在しません。ファイルを配置して下さい。")
            Me.Close()
            Exit Sub
        End If

        '画面サイズ等
        Me.WindowState = FormWindowState.Maximized
        Me.MinimizeBox = False
        Me.MaximizeBox = False

        '画像の配置処理
        topPicture.ImageLocation = imageFilePath
    End Sub

    ''' <summary>
    ''' トップ画像クリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub topPicture_Click(sender As System.Object, e As System.EventArgs) Handles topPicture.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 事業所マスタボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOfficeMaster_Click(sender As System.Object, e As System.EventArgs) Handles btnOfficeMaster.Click
        If IsNothing(officeMasterForm) OrElse officeMasterForm.IsDisposed Then
            officeMasterForm = New 事業所マスタ()
            officeMasterForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 受診者マスタボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExamineeMaster_Click(sender As System.Object, e As System.EventArgs) Handles btnExamineeMaster.Click
        If IsNothing(examineeMasterForm) OrElse examineeMasterForm.IsDisposed Then
            examineeMasterForm = New 受診者マスタ()
            examineeMasterForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 健診結果FDボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnResultFD_Click(sender As System.Object, e As System.EventArgs) Handles btnResultFD.Click
        If IsNothing(resultFDForm) OrElse resultFDForm.IsDisposed Then
            resultFDForm = New 健診結果ＦＤ()
            resultFDForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 受診者一覧ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExamineeList_Click(sender As System.Object, e As System.EventArgs) Handles btnExamineeList.Click
        If IsNothing(examineeListForm) OrElse examineeListForm.IsDisposed Then
            examineeListForm = New 受診者一覧()
            examineeListForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 健診結果報告書ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnResultReport_Click(sender As System.Object, e As System.EventArgs) Handles btnResultReport.Click
        If IsNothing(resultReportForm) OrElse resultReportForm.IsDisposed Then
            resultReportForm = New 健診結果報告書()
            resultReportForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' 事業所別実施履歴ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnImplementationHistory_Click(sender As System.Object, e As System.EventArgs) Handles btnImplementationHistory.Click
        If IsNothing(implementationHistoryForm) OrElse implementationHistoryForm.IsDisposed Then
            implementationHistoryForm = New 事業所別実施履歴()
            implementationHistoryForm.Show()
        End If
    End Sub
End Class
