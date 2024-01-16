Public Class StudentCourse
    Dim unerollPanel As New Panel
    Private Sub StudentCourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.Components(Me, 908, 174)
        InitializeComponent()
    End Sub
    Sub New(courseId As String, courseNumber As String, courseName As String, courseTeacher As String, courseUnits As String, classCode As String, description As String)
        InitializeComponent()
        CreateCourses(courseId, courseNumber, courseName, courseTeacher, courseUnits, classCode, description)
        ModifyButton()
        UnenrollButton(courseId)
    End Sub
    Private Sub CreateCourses(courseId As String, courseNumber As String, courseName As String, courseTeacher As String, courseUnits As String, classCode As String, description As String)
        Dim courseNumberLbl As Label = Tools.CreateUserLabel(Me, 300, 40, 35, 21)
        courseNumberLbl.Text = courseNumber
        courseNumberLbl.Font = New Font("Poppins", 18, FontStyle.Bold)

        Dim courseNameLbl As Label = Tools.CreateUserLabel(Me, 350, 40, 39, 68)
        courseNameLbl.Text = courseName
        courseNameLbl.Font = New Font("Poppins", 12, FontStyle.Regular)

        Dim courseTeacherLbl As Label = Tools.CreateUserLabel(Me, 300, 40, 39, 120)
        courseTeacherLbl.Text = courseTeacher
        courseTeacherLbl.Font = New Font("Poppins", 12, FontStyle.Regular)

        Dim courseUnitsLbl As Label = Tools.CreateUserLabel(Me, 350, 40, 525, 21)
        courseUnitsLbl.Text = courseUnits
        courseUnitsLbl.Font = New Font("Poppins", 18, FontStyle.Bold)
        courseUnitsLbl.TextAlign = ContentAlignment.MiddleRight

        Dim viewBtn As Label = Tools.CreateButton(Me, 140, 32, 729, 121)
        AddHandler viewBtn.Click, Sub()
                                      StudentDashboard.ViewClassCourse(courseId, courseNumber, courseName, description, classCode)
                                  End Sub
    End Sub
    Private Sub ModifyButton()
        Dim button As Label = Tools.CreateButton(Me, 7, 25, 862, 19)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.OpenPanel(unerollPanel)
                                 End Sub
    End Sub
    Private Sub UnenrollButton(courseId As String)
        unerollPanel = Tools.CreatePanel(Me, 153, 35, 704, 15)
        Dim button As Label = Tools.CreateButton(Me, 120, 35, 5, 6)
        button.BackColor = ColorTranslator.FromHtml("#FF4141")
        button.Text = "Unenroll"
        button.ForeColor = Color.White
        button.Font = New Font("Poppins", 8, FontStyle.Bold)
        button.TextAlign = ContentAlignment.MiddleCenter
        button.BringToFront()
        unerollPanel.Controls.Add(button)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     ' NEED TO FIX 
                                     Handlers.TestButton()
                                 End Sub
    End Sub
End Class
