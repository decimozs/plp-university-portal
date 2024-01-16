Public Class TodoOverviewComponent
    Dim todoTitle As New Label
    Dim todoAbout As New Label
    Private Sub TodoOverviewComponent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 621, 125)
    End Sub
    Sub New(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As String)
        InitializeComponent()
        CreateAnnouncementOverviewComponent(title, deadline)
        ViewButton(id, title, description, filename, filepath, createdAt, deadline, grade, teacherNo)
    End Sub
    Private Sub CreateAnnouncementOverviewComponent(title As String, deadline As String)
        todoTitle = Tools.CreateUserLabel(Me, 300, 34, 27, 27)
        todoTitle.Text = title
        todoTitle.Font = New Font("Poppins", 11, FontStyle.Bold)

        todoAbout = Tools.CreateUserLabel(Me, 300, 34, 27, 64)
        todoAbout.Text = $"Deadline: {deadline}"
        todoAbout.Font = New Font("Poppins", 11, FontStyle.Bold)
        todoAbout.ForeColor = ColorTranslator.FromHtml("#FF4141")
    End Sub
    Private Sub ViewButton(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As String)
        Dim button As Label = Tools.CreateButton(Me, 117, 37, 475, 70)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     StudentDashboard.ViewTodo(id, title, description, filename, filepath, createdAt, deadline, grade, teacherNo)
                                 End Sub
    End Sub
End Class
