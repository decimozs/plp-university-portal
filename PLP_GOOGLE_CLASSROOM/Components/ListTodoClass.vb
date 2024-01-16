Public Class ListTodoClass
    Private Sub ListTodoClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1019, 170)
    End Sub
    Sub New(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, teacherNo As String, studentName As String, grade As String)
        InitializeComponent()
        CreateListTodoClass(title, description, deadline)
        ViewButton(id, title, description, filename, filepath, createdAt, deadline, teacherNo, studentName, grade)
    End Sub
    Private Sub CreateListTodoClass(title As String, description As String, deadline As String)
        Dim titleLbl As Label = Tools.CreateUserLabel(Me, 409, 56, 38, 26)
        titleLbl.Text = title
        titleLbl.Font = New Font("Poppins", 22, FontStyle.Regular)

        Dim descriptionLbl As Label = Tools.CreateUserLabel(Me, 724, 36, 38, 108)
        descriptionLbl.Text = description
        descriptionLbl.Font = New Font("Poppins", 11, FontStyle.Regular)
        descriptionLbl.ForeColor = ColorTranslator.FromHtml("#9C8B8B")

        Dim deadlineLbl As Label = Tools.CreateUserLabel(Me, 257, 32, 730, 26)
        deadlineLbl.Text = deadline
        deadlineLbl.Font = New Font("Poppins", 11, FontStyle.Regular)
        deadlineLbl.TextAlign = ContentAlignment.MiddleRight
        deadlineLbl.ForeColor = ColorTranslator.FromHtml("#FD5D5D")
    End Sub
    Private Sub ViewButton(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, teacherNo As String, studentName As String, grade As String)
        Dim button As Label = Tools.CreateButton(Me, 140, 32, 842, 112)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.ViewListTodo(id, title, description, filename, filepath, createdAt, deadline, teacherNo, studentName, grade)
                                 End Sub
    End Sub
End Class
