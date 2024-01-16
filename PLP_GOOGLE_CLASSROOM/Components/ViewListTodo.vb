Public Class ViewListTodo
    Private Sub ViewListTodo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
    End Sub
    Sub New(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, teacherNo As String, studentName As String, grade As String)
        InitializeComponent()
        CreateViewListTodoClass(id, title, description, filename, filepath, createdAt, deadline, teacherNo, studentName, grade)
        BackButton()
    End Sub
    Private Sub CreateViewListTodoClass(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, teacherNo As String, studentName As String, grade As String)
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
        Dim button As Label = Tools.CreateButton(Me, 35, 28, 1559, 19)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.listTodoPanel.Visible = False
                                 End Sub
    End Sub
End Class
