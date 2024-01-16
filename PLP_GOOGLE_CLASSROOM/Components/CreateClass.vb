Public Class CreateClass
    Dim uniqueClassCode As String
    Public tableName = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Public teacherName = Handlers.GetTeacherName(tableName)
    Dim courseProgram As New TextBox
    Dim courseName As New TextBox
    Dim courseDescription As New TextBox
    Private Sub CreateClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 676, 636)
        uniqueClassCode = Handlers.GenerateUniqueClassNumber()
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CloseButton()
        CourseProgramTextBox()
        CourseNameTextBox()
        CourseDescriptionTextBox()
        CreateCourseButton()
    End Sub
    Private Sub CloseButton()
        Dim closeBtn As Label = Tools.CreateButton(Me, 42, 24, 594, 28)
        AddHandler closeBtn.Click, Sub(sender As Object, e As EventArgs)
                                       TeacherDashboard.createCoursePanelPopup.Visible = False
                                   End Sub
    End Sub
    Private Sub CourseProgramTextBox()
        courseProgram = Tools.CreateTextBox(Me, "Enter your course program", 550, 50, 60, 180)
    End Sub
    Private Sub CourseNameTextBox()
        courseName = Tools.CreateTextBox(Me, "Enter your course name", 550, 50, 60, 284)
    End Sub
    Private Sub CourseDescriptionTextBox()
        courseDescription = Tools.CreateTextBox(Me, "Enter your course description", 550, 50, 60, 388)
    End Sub
    Private Sub CreateCourseButton()
        Dim button As Label = Tools.CreateButton(Me, 591, 95, 39, 496)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.CreateClassCourse(tableName, uniqueClassCode, courseProgram.Text, courseName.Text, courseDescription.Text, "3 Units", teacherName, tableName)
                                     TeacherDashboard.createCoursePanelPopup.Visible = False
                                     TeacherDashboard.createCoursePanelPopup.BringToFront()
                                     TeacherDashboard.popUpPanel.Visible = True
                                     TeacherDashboard.PopUp()
                                 End Sub
    End Sub
End Class
