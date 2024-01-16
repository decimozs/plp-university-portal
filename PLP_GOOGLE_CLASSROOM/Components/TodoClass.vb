Public Class TodoClass
    Private Sub TodoClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1019, 123)
    End Sub
    Sub New(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, teacherNo As String)
        InitializeComponent()
        CreateTodoClass(id, title, description, filename, filepath, createdAt, deadline, teacherNo)
    End Sub
    Private Sub CreateTodoClass(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, teacherNo As String)
        Dim titleLbl As Label = Tools.CreateUserLabel(Me, 134, 36, 48, 26)
        titleLbl.Text = title
        titleLbl.Font = New Font("Poppins", 11, FontStyle.Bold)

        Dim descriptionLbl As Label = Tools.CreateUserLabel(Me, 424, 36, 48, 62)
        descriptionLbl.Text = description
        descriptionLbl.ForeColor = ColorTranslator.FromHtml("#9C8B8B")
        descriptionLbl.Font = New Font("Poppins", 11, FontStyle.Regular)

        Dim deadlineLbl As Label = Tools.CreateUserLabel(Me, 200, 32, 780, 59)
        deadlineLbl.Text = deadline
        deadlineLbl.ForeColor = ColorTranslator.FromHtml("#FF4141")
        deadlineLbl.Font = New Font("Poppins", 11, FontStyle.Bold)
        deadlineLbl.TextAlign = ContentAlignment.MiddleRight

    End Sub
End Class
