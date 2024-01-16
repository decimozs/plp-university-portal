Imports System.IO

Public Class CreateTodo
    Public tableName = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Public sTableName = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Dim currentDateAndTime As DateTime = DateTime.Now
    Dim inputTodoTitle As New TextBox
    Dim inputTodoAbout As New TextBox
    Dim inputMonthh As New TextBox
    Dim inputDay As New TextBox
    Dim inputHour As New TextBox
    Dim inputMinute As New TextBox
    Dim inputAmOrPm As New TextBox
    Dim attachFile As New TextBox
    Dim attachFilePath As String = String.Empty
    Private Sub CreateTodo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 676, 963)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateTodoClass()
        BackButton()
        PostTodoButton()
    End Sub
    Private Sub CreateTodoClass()
        inputTodoTitle = Tools.CreateTextBox(Me, "Todo titile", 560, 50, 59, 180)

        inputTodoAbout = Tools.CreateTextBox(Me, "Todo about", 560, 180, 59, 284)
        inputTodoAbout.Multiline = True

        inputMonthh = Tools.CreateTextBox(Me, "Month", 200, 50, 59, 532)

        inputDay = Tools.CreateTextBox(Me, "Day", 200, 50, 383, 532)

        inputHour = Tools.CreateTextBox(Me, "Hour", 149, 50, 59, 595)

        inputMinute = Tools.CreateTextBox(Me, "Minute", 110, 50, 308, 595)

        inputAmOrPm = Tools.CreateTextBox(Me, "AM", 39, 50, 531, 595)

        attachFile = Tools.CreateTextBox(Me, "Find a file", 500, 50, 59, 699)
        attachFile.ReadOnly = True
        AddHandler attachFile.Click, Sub(sender As Object, e As EventArgs)
                                         OpenFileExplorer()
                                     End Sub
    End Sub
    Private Sub BackButton()
        Dim button As Label = Tools.CreateButton(Me, 40, 25, 594, 28)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.createTodoPanelPopup.Visible = False
                                 End Sub
    End Sub
    Private Sub OpenFileExplorer()
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.InitialDirectory = "C:\"
        openFileDialog.Filter = "PDF Files|*.pdf|All Files|*.*"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim selectedFilePath As String = openFileDialog.FileName

            Dim fileName As String = Path.GetFileName(selectedFilePath)

            attachFile.Text = fileName

            attachFilePath = selectedFilePath
        End If
    End Sub
    Private Sub PostTodoButton()
        Dim button As Label = Tools.CreateButton(Me, 591, 109, 39, 820)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.CreateClassTodo(tableName, inputTodoTitle.Text, inputTodoAbout.Text, attachFile.Text, attachFilePath, currentDateAndTime.ToString(), $"{inputMonthh.Text} {inputDay.Text} - {inputHour.Text}:{inputMinute.Text} {inputAmOrPm.Text}", "0", tableName)
                                     Handlers.CreateStudentTodo(sTableName, inputTodoTitle.Text, inputTodoAbout.Text, attachFile.Text, attachFilePath, currentDateAndTime.ToString(), $"{inputMonthh.Text} {inputDay.Text} - {inputHour.Text}:{inputMinute.Text} {inputAmOrPm.Text}", "0", tableName)
                                     TeacherDashboard.createTodoPanelPopup.Visible = False
                                 End Sub
    End Sub
End Class
