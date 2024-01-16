Public Class TeacherGradedView
    Public inputGrade As New TextBox
    Private Sub TeacherGradedView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
    End Sub
    Sub New(id As String, title As String, studentName As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As String)
        InitializeComponent()
        CreateTodoClass(title, studentName, description, filename, filepath, createdAt, deadline, teacherNo)
        BackButton()
        GradedButton(id)
        GradedTextBox(grade)
    End Sub
    Private Sub CreateTodoClass(title As String, studentName As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, teacherNo As String)
        Dim titleLbl As Label = Tools.CreateUserLabel(Me, 509, 64, 100, 79)
        titleLbl.Text = studentName
        titleLbl.Font = New Font("Poppins", 22, FontStyle.Bold)

        Dim createAtLbl As Label = Tools.CreateUserLabel(Me, 509, 37, 108, 143)
        createAtLbl.Text = createdAt
        createAtLbl.Font = New Font("Poppins", 11, FontStyle.Regular)

        ' test
        Dim descriptionLbl As Label = Tools.CreateUserLabel(Me, 706, 241, 108, 208)
        descriptionLbl.Text = title
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
    Private Sub BackButton()
        Dim button As Label = Tools.CreateButton(Me, 74, 23, 1520, 23)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.gradedViewPanel.Visible = False
                                 End Sub
    End Sub
    Private Sub GradedTextBox(grade As String)
        inputGrade = Tools.CreateTextBox(Me, grade, 80, 44, 1452, 508)
        inputGrade.TextAlign = HorizontalAlignment.Center
    End Sub
    Private Sub GradedButton(id As String)
        Dim button As Label = Tools.CreateButton(Me, 352, 78, 1188, 578)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.gradedViewPanel.Visible = False
                                     Handlers.UpdateTodoGrade("2200638", id, inputGrade.Text, "2200648")
                                 End Sub
    End Sub
End Class
