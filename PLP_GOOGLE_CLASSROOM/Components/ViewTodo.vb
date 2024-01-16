Imports System.IO
Imports iText.Kernel.Pdf.Colorspace.PdfDeviceCs

Public Class ViewTodo
    Dim parseId As String = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Dim attachFile As New TextBox
    Dim attachFilePath As String = String.Empty
    Dim studentNameText As String = Handlers.GetStudentName(parseId)
    Dim currentDateAndTime As DateTime = DateTime.Now
    Public inputGrade As New TextBox
    Private Sub ViewTodo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
    End Sub
    Sub New(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As String)
        InitializeComponent()
        CreateTodoClass(title, description, filename, filepath, createdAt, deadline, grade, teacherNo)
        BackButton()
        AttachFileButton()
        SubmitButton(id, title, description, filename, filepath, createdAt, deadline, grade, teacherNo)
        GradedTextBox(grade)
    End Sub
    Private Sub CreateTodoClass(title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As String)
        Dim titleLbl As Label = Tools.CreateUserLabel(Me, 509, 64, 100, 79)
        titleLbl.Text = title
        titleLbl.Font = New Font("Poppins", 22, FontStyle.Bold)

        Dim createAtLbl As Label = Tools.CreateUserLabel(Me, 509, 37, 108, 143)
        createAtLbl.Text = createdAt
        createAtLbl.Font = New Font("Poppins", 11, FontStyle.Regular)

        ' test
        Dim descriptionLbl As Label = Tools.CreateUserLabel(Me, 706, 241, 108, 208)
        descriptionLbl.Text = description
        descriptionLbl.Font = New Font("Poppins", 14, FontStyle.Regular)

        Dim filenameLbl As Label = Tools.CreateUserLabel(Me, 216, 42, 168, 560)
        filenameLbl.Text = filename
        filenameLbl.Font = New Font("Poppins", 8, FontStyle.Bold)
        filenameLbl.ForeColor = Color.White

        Dim deadlineLbl As Label = Tools.CreateUserLabel(Me, 509, 37, 100, 654)
        deadlineLbl.Text = $"Deadline: {deadline}"
        deadlineLbl.Font = New Font("Poppins", 11, FontStyle.Bold)
        deadlineLbl.ForeColor = ColorTranslator.FromHtml("#FF4141")
    End Sub
    Private Sub GradedTextBox(grade As String)
        inputGrade = Tools.CreateTextBox(Me, grade, 80, 44, 1450, 467)
        inputGrade.TextAlign = HorizontalAlignment.Center
    End Sub
    Private Sub BackButton()
        Dim button As Label = Tools.CreateButton(Me, 74, 23, 1520, 23)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     StudentDashboard.viewTodoPanel.Visible = False
                                 End Sub
    End Sub
    Private Sub SubmitButton(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As String)
        Dim button As Label = Tools.CreateButton(Me, 352, 78, 1188, 578)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     StudentDashboard.viewTodoPanel.Visible = False
                                     Handlers.SubmitStudentTodo(parseId, id, studentNameText, title, currentDateAndTime.ToString, grade, filename, filepath, teacherNo)
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
    Private Sub AttachFileButton()
        attachFile = Tools.CreateTextBox(Me, "Find a file", 260, 42, 1270, 527)
        attachFile.ReadOnly = True
        attachFile.BackColor = ColorTranslator.FromHtml("#047F48")
        attachFile.ForeColor = Color.White
        AddHandler attachFile.Click, Sub(sender As Object, e As EventArgs)
                                         attachFile.ForeColor = Color.White
                                         OpenFileExplorer()
                                     End Sub
    End Sub
End Class
