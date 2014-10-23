Imports System.Text

Public Class Form1
    Private _fieldCode As New Dictionary(Of String, String)

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If _fieldCode.ContainsKey(txtField.Text) Then
            If MsgBox("字段名重复，是否覆盖原内容？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '覆盖原内容
                _fieldCode.Item(txtField.Text) = txtCode.Text
            Else
                txtField.SelectAll()
                txtField.Focus()
                Exit Sub
            End If
        Else
            '第一次添加
            _fieldCode.Add(txtField.Text, txtCode.Text)
            lstSource.Items.Add(txtField.Text)
        End If
        '清空
        txtField.Clear()
        txtCode.Clear()
        '聚焦
        txtField.Focus()
    End Sub

    Private Sub lstSource_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSource.Click
        If lstSource.Items.Count > 0 AndAlso lstSource.SelectedIndex >= 0 Then
            lstDest.Items.Add(lstSource.Items(lstSource.SelectedIndex).ToString())
            lstSource.Items.RemoveAt(lstSource.SelectedIndex)
        End If
        txtDestCode.Clear()
        txtDestCode.AppendText(CreateCode())
    End Sub

    Private Sub lstDest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDest.Click
        If lstDest.Items.Count > 0 AndAlso lstDest.SelectedIndex >= 0 Then
            lstSource.Items.Add(lstDest.Items(lstDest.SelectedIndex).ToString())
            lstDest.Items.RemoveAt(lstDest.SelectedIndex)
        End If
        txtDestCode.Clear()
        txtDestCode.AppendText(CreateCode())
    End Sub

    Public Function CreateCode() As String
        Dim htmlCode As New StringBuilder
        If lstDest.Items.Count > 0 Then
            For i As Integer = 0 To lstDest.Items.Count - 1
                htmlCode.AppendLine(_fieldCode.Item(lstDest.Items(i)))
            Next
        End If

        Return htmlCode.ToString()
    End Function
End Class
