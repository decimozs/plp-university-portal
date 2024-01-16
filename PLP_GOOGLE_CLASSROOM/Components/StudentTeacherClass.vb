Public Class StudentTeacherClass
    Private Sub StudentTeacherClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1019, 207)
    End Sub
    Sub New(id As String, teacherName As String, teacherRole As String, courseNumber As String, courseName As String)
        InitializeComponent()
        CreateStudentTeacherClass(id, teacherName, teacherRole, courseNumber, courseName)
    End Sub
    Private Sub CreateStudentTeacherClass(id As String, teacherName As String, teacherRole As String, courseNumber As String, courseName As String)
        Dim nameLbl As Label = Tools.CreateUserLabel(Me, 500, 33, 200, 53)
        nameLbl.Text = teacherName
        nameLbl.Font = New Font("Poppins", 16, FontStyle.Bold)

        Dim roleLbl As Label = Tools.CreateUserLabel(Me, 500, 33, 205, 86)
        Dim role As String = Handlers.GetTeacherPosition(id)
        roleLbl.Text = role
        roleLbl.Font = New Font("Poppins", 11, FontStyle.Regular)

        Dim courseNumberLbl As Label = Tools.CreateUserLabel(Me, 300, 31, 680, 124)
        courseNumberLbl.Text = courseNumber
        courseNumberLbl.Font = New Font("Poppins", 11, FontStyle.Regular)
        courseNumberLbl.ForeColor = ColorTranslator.FromHtml("#959595")
        courseNumberLbl.TextAlign = ContentAlignment.MiddleRight

        Dim courseNameLbl As Label = Tools.CreateUserLabel(Me, 300, 53, 685, 140)
        courseNameLbl.Text = courseName
        courseNameLbl.Font = New Font("Poppins", 16, FontStyle.Bold)
        courseNameLbl.TextAlign = ContentAlignment.MiddleRight
    End Sub
End Class
