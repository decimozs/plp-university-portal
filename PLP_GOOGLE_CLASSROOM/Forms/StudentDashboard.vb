
Public Class StudentDashboard
    Public parseId = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Public joinClassPopup As New Panel
    Public menuPanel As New Panel
    Public coursesPanel As New Panel
    Public todoPanel As New Panel
    Public gradesPanel As New Panel
    Public calendarPanel As New Panel
    Public teacherPanel As New Panel
    Public discussionForumPanel As New Panel
    Public announcementPanel As New Panel
    Public helpPanel As New Panel
    Public noAvailableCoursesPanel As New Panel
    Public availableCoursesPanel As New FlowLayoutPanel
    Public dashboardPanel As New Panel
    Public viewTodoPanel As New Panel
    Public classCourse As New Panel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.Dashboard(Me)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        JoinClassPopupPanel()
        Menu()
        MenuButton()
        DashboardOverviewPanel()
        GreetingsUser()
        Semester()
        DateNow()
        CloseButton()
        MinimizedButton()
        StudentName()
        StudentYear()
    End Sub
    Private Sub CloseButton()
        Tools.CloseButton(Me, 36, 43, 1749, 2)
    End Sub
    Private Sub MinimizedButton()
        Tools.MinimizedButton(Me, 36, 43, 1704, 2)
    End Sub
    Private Sub StudentName()
        Dim studentNameLbl = Tools.CreateUserLabel(Me, 926, 25, 233, 85)
        studentNameLbl.text = Handlers.GetStudentName(parseId)
        studentNameLbl.Font = New Font("Poppins", 9, FontStyle.Bold)
    End Sub
    Private Sub StudentYear()
        Dim studentYearLbl As Label = Tools.CreateUserLabel(Me, 500, 25, 233, 105)
        studentYearLbl.ForeColor = ColorTranslator.FromHtml("#9C8B8B")
        studentYearLbl.Text = Handlers.GetStudentYear(parseId)
        studentYearLbl.Font = New Font("Poppins", 9, FontStyle.Bold)
    End Sub
    Private Sub DateNow()
        Dim currentDate As DateTime = DateTime.Now
        Dim formattedDate As String = currentDate.ToString("MMMM d, yyyy")
        Dim getCurrentDateLbl As Label = Tools.CreateUserLabel(Me, 200, 26, 151, 252)
        getCurrentDateLbl.ForeColor = ColorTranslator.FromHtml("#9C8B8B")
        getCurrentDateLbl.Text = formattedDate
        getCurrentDateLbl.Font = New Font("Poppins", 11, FontStyle.Bold)
    End Sub
    Private Sub Semester()
        Dim semesterLbl As Label = Tools.CreateUserLabel(Me, 767, 70, 937, 231)
        semesterLbl.ForeColor = Color.White
        semesterLbl.Text = Handlers.GetStudentSemester(parseId)
        semesterLbl.Font = New Font("Poppins", 30, FontStyle.Bold)
        semesterLbl.TextAlign = ContentAlignment.MiddleRight
    End Sub
    Private Sub GreetingsUser()
        Dim greetingsLbl As Label = Tools.CreateUserLabel(Me, 767, 70, 934, 361)
        greetingsLbl.ForeColor = Color.White
        greetingsLbl.Text = $"Welcome Back, {Handlers.GetStudentFirstName(parseId)}!"
        greetingsLbl.Font = New Font("Poppins", 20, FontStyle.Bold)
        greetingsLbl.TextAlign = ContentAlignment.MiddleRight

        Dim text As Label = Tools.CreateUserLabel(Me, 767, 30, 925, 425)
        text.ForeColor = Color.White
        text.Text = "Always stay updated in your student portal"
        text.Font = New Font("Poppins", 11, FontStyle.Regular)
        text.TextAlign = ContentAlignment.MiddleRight
    End Sub
    Private Sub Menu()
        menuPanel = Tools.CreatePanel(Me, 389, 962, 0, 44)
        Dim menuPanelInstance As New StudentMenu
        menuPanel.Controls.Add(menuPanelInstance)
    End Sub
    Private Sub MenuButton()
        Dim menuBtn As Label = Tools.CreateButton(Me, 35, 106, 0, 82)
        AddHandler menuBtn.MouseEnter, Sub(sender As Object, e As EventArgs)
                                           menuPanel.Visible = True
                                           menuPanel.BringToFront()
                                       End Sub

        AddHandler menuPanel.MouseLeave, Sub(sender As Object, e As EventArgs)
                                             If Not menuBtn.ClientRectangle.Contains(menuBtn.PointToClient(Control.MousePosition)) Then
                                                 menuPanel.Visible = False
                                             End If
                                         End Sub
    End Sub
    Private Sub JoinClassPopupPanel()
        joinClassPopup = Tools.CreatePanel(Me, 676, 636, 574, 248)
        Dim joinClassInstance As New JoinClass
        joinClassPopup.Controls.Add(joinClassInstance)
        joinClassPopup.BringToFront()
    End Sub
    Private Sub NoAvailableEnrooledCoursePanel()
        noAvailableCoursesPanel = Tools.CreatePanel(Me, 970, 364, 98, 566)
        Dim noCourseAvailableInstance As New NoCoursesAvailable
        noAvailableCoursesPanel.Controls.Add(noCourseAvailableInstance)
        noAvailableCoursesPanel.Visible = True
    End Sub
    Private Sub OverviewEnrolledCourses()
        Dim courses As List(Of StudentClassCourse) = FetchAllStudentClassCourse(parseId)

        Dim panelWidth = 908
        Dim panelHeight = 174
        Dim spaceBetweenPanels As Integer = 10

        availableCoursesPanel.BackColor = Color.Transparent
        availableCoursesPanel.Size = New Size(1000, 365)
        availableCoursesPanel.Location = New Point(98, 565)
        availableCoursesPanel.FlowDirection = FlowDirection.LeftToRight
        availableCoursesPanel.WrapContents = True

        If courses.Count > 0 Then
            For Each course In courses
                Dim xCoordinate As Integer = 0
                Dim yCoordinate As Integer = If(availableCoursesPanel.Controls.Count > 0, availableCoursesPanel.Controls(availableCoursesPanel.Controls.Count - 1).Bottom + spaceBetweenPanels, 0)
                Dim coursePanel As Panel = Tools.CreatePanel(availableCoursesPanel, panelWidth, panelHeight, xCoordinate, yCoordinate)
                Handlers.PopulateStudentClassCourse(coursePanel, courses, courses.IndexOf(course))
                availableCoursesPanel.Controls.Add(coursePanel)
            Next
        Else
            NoAvailableEnrooledCoursePanel()
        End If
        Me.Controls.Add(availableCoursesPanel)
    End Sub
    Public Sub ViewTodo(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As String)
        viewTodoPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        viewTodoPanel.BringToFront()
        Dim viewTodoInstance As New ViewTodo(id, title, deadline, filename, filepath, createdAt, deadline, grade, teacherNo)
        viewTodoPanel.Controls.Add(viewTodoInstance)
        viewTodoPanel.Visible = True
    End Sub
    Public Sub ViewClassCourse(id As String, program As String, name As String, description As String, classCode As String)
        classCourse = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        classCourse.BringToFront()
        Dim viewClassCourseInstance As New ViewStudentClassCourse(id, program, name, description, classCode)
        classCourse.Controls.Add(viewClassCourseInstance)
        classCourse.Visible = True
    End Sub
    Public Sub DashboardOverviewPanel()
        dashboardPanel = Tools.CreatePanel(Me, 1631, 774, 98, 182)
        dashboardPanel.BringToFront()
        Dim dashboardInstance As New StudentOverviewDashboard
        dashboardPanel.Controls.Add(dashboardInstance)
        dashboardPanel.Visible = True
    End Sub
    Public Sub CoursesDashboardPanel()
        coursesPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        coursesPanel.BringToFront()
        Dim classCourseInstance As New StudentCourseDashboard
        coursesPanel.Controls.Add(classCourseInstance)
        coursesPanel.Visible = True
    End Sub
    Public Sub TodoDashboardPanel()
        todoPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        todoPanel.BringToFront()
        Dim todoInstance As New StudentAssignTodoDashboard
        todoPanel.Controls.Add(todoInstance)
        todoPanel.Visible = True
    End Sub
    Public Sub GradesDashboardPanel()
        gradesPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        gradesPanel.BringToFront()
        Dim gradesInstance As New StudentGradesDashboard
        gradesPanel.Controls.Add(gradesInstance)
        gradesPanel.Visible = True
    End Sub
    Public Sub CalendarDashboardPanel()
        calendarPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        calendarPanel.BringToFront()
        Dim calendarInstance As New StudentCalendarDashboard
        calendarPanel.Controls.Add(calendarInstance)
        calendarPanel.Visible = True
    End Sub
    Public Sub TeacherDashboardPanel()
        teacherPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        teacherPanel.BringToFront()
        Dim teacherInstance As New StudentTeacherDashboard
        teacherPanel.Controls.Add(teacherInstance)
        teacherPanel.Visible = True
    End Sub
    Public Sub DiscussionForumDashboardPanel()
        discussionForumPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        discussionForumPanel.BringToFront()
        Dim discussionDashboardInstance As New StudentDiscussionForumDashboard
        discussionForumPanel.Controls.Add(discussionDashboardInstance)
        discussionForumPanel.Visible = True
    End Sub
    Public Sub AnnouncementDashboardPanel()
        announcementPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        announcementPanel.BringToFront()
        Dim annoucementDashboardInstance As New StudentAnnouncementDashboard
        announcementPanel.Controls.Add(annoucementDashboardInstance)
        announcementPanel.Visible = True
    End Sub
    Public Sub HelpDashboardPanel()
        helpPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        helpPanel.BringToFront()
        Dim helpDashboardInstance As New HelpDashboard
        helpPanel.Controls.Add(helpDashboardInstance)
        helpPanel.Visible = True
    End Sub
End Class