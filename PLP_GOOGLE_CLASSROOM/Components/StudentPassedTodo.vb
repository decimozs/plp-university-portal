Imports iText.StyledXmlParser.Jsoup.Select.Evaluator

Public Class StudentPassedTodo
    Private Sub StudentPassedTodo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1019, 123)
    End Sub
    Sub New(id As String, title As String, studentName As String, description As String, createdAt As String, filename As String, filepath As String, deadline As String, grade As String, teacherNo As String)
        InitializeComponent()
        CreateStudentPassedTodo(title, studentName)
        ViewButton(id, title, studentName, description, createdAt, filename, filepath, deadline, grade, teacherNo)
    End Sub
    Private Sub CreateStudentPassedTodo(title As String, studentName As String)
        Dim titleLbl As Label = Tools.CreateUserLabel(Me, 90, 24, 139, 33)
        titleLbl.Text = title
        titleLbl.Font = New Font("Poppins", 10, FontStyle.Regular)
        titleLbl.ForeColor = ColorTranslator.FromHtml("#959595")

        Dim studentNameLbl As Label = Tools.CreateUserLabel(Me, 503, 36, 139, 55)
        studentNameLbl.Text = studentName
        studentNameLbl.Font = New Font("Poppins", 10, FontStyle.Bold)
    End Sub
    Private Sub ViewButton(id As String, title As String, studentName As String, description As String, createdAt As String, filename As String, filepath As String, deadline As String, grade As String, teacherNo As String)
        Dim button As Label = Tools.CreateButton(Me, 140, 32, 857, 72)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.ViewGradedTodo(id, title, studentName, description, filename, filepath, createdAt, deadline, grade, teacherNo)
                                 End Sub
    End Sub
End Class
