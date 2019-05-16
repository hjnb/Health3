Imports System.Text

Public Class resultInputDataGridView
    Inherits DataGridView

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

    Private Sub resultInputDataGridView_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellEndEdit
        Dim currentCellRowIndex As Integer = Me.CurrentCell.RowIndex
        Dim currentCellColumnIndex As Integer = Me.CurrentCell.ColumnIndex

        '身長 or 体重を入力後、標準体重とbmi算出、BMIの値で指導区分を算出
        If currentCellColumnIndex = 4 AndAlso (currentCellRowIndex = 0 OrElse currentCellRowIndex = 1) Then
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
