Public Class StudentOverviewDashboard
    Public parseId = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Public availableTodoPanel As New FlowLayoutPanel
    Public availableCoursesPanel As New FlowLayoutPanel
    Public noAvailableTodoPanel As New Panel
    Public noAvailableCoursesPanel As New Panel
    Dim seeAllBtn As New Label
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentOverviewDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1640, 774)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateOverviewDashboard()
        OverViewAnnouncementPanel()
        OverviewEnrolledCourses()
    End Sub
    Private Sub CreateOverviewDashboard()
        Dim currentDate As DateTime = DateTime.Now
        Dim formattedDate As String = currentDate.ToString("MMMM d, yyyy")
        Dim getCurrentDateLbl As Label = Tools.CreateUserLabel(Me, 200, 26, 52, 65)
        getCurrentDateLbl.ForeColor = ColorTranslator.FromHtml("#9C8B8B")
        getCurrentDateLbl.Text = formattedDate
        getCurrentDateLbl.Font = New Font("Poppins", 11, FontStyle.Bold)

        Dim teacherDepartmentLbl As Label = Tools.CreateUserLabel(Me, 1000, 70, 600, 44)
        teacherDepartmentLbl.ForeColor = Color.White
        teacherDepartmentLbl.Text = Handlers.GetStudentSemester(parseId)
        teacherDepartmentLbl.Font = New Font("Poppins", 30, FontStyle.Bold)
        teacherDepartmentLbl.TextAlign = ContentAlignment.MiddleRight

        Dim greetingsLbl As Label = Tools.CreateUserLabel(Me, 767, 70, 828, 174)
        greetingsLbl.ForeColor = Color.White
        greetingsLbl.Text = $"Welcome Back, {Handlers.GetStudentFirstName(parseId)}!"
        greetingsLbl.Font = New Font("Poppins", 20, FontStyle.Bold)
        greetingsLbl.TextAlign = ContentAlignment.MiddleRight

        Dim text As Label = Tools.CreateUserLabel(Me, 767, 30, 820, 234)
        text.ForeColor = Color.White
        text.Text = "Always stay updated in your student portal"
        text.Font = New Font("Poppins", 11, FontStyle.Regular)
        text.TextAlign = ContentAlignment.MiddleRight
    End Sub
    Private Sub NoAvailablePendingTodosPanel()
        noAvailableTodoPanel = Tools.CreatePanel(Me, 621, 364, 1010, 377)
        Dim noTodoAvailableInstance As New NoTodoAvailable
        noAvailableTodoPanel.Controls.Add(noTodoAvailableInstance)
        noAvailableTodoPanel.Visible = True
    End Sub
    Public Sub OverViewAnnouncementPanel()
        Dim todos As List(Of Todo) = FetchAvailableTodo("2200648")

        Dim panelWidth = 621
        Dim panelHeight = 125
        Dim spaceBetweenPanels As Integer = 10

        availableTodoPanel.BackColor = Color.Transparent
        availableTodoPanel.Size = New Size(625, 275)
        availableTodoPanel.Location = New Point(1003, 379)
        availableTodoPanel.FlowDirection = FlowDirection.TopDown
        availableTodoPanel.WrapContents = False

        If todos.Count > 0 Then
            For Each todo In todos
                Dim todoClassPanel As Panel = Tools.CreatePanel(availableTodoPanel, panelWidth, panelHeight, 0, 0)
                Handlers.PopulateAvailableTodo(todoClassPanel, todos, todos.IndexOf(todo))
                availableTodoPanel.Controls.Add(todoClassPanel)
            Next
            SeeAllAnnouncmentButton()
        Else
            NoAvailablePendingTodosPanel()
        End If

        Me.Controls.Add(availableTodoPanel)
    End Sub
    Private Sub SeeAllAnnouncmentButton()
        Dim panel As Panel = Tools.CreatePanel(Me, 621, 78, 1005, 658)
        Dim seeAllButtonInstance As New SeeAllButton()
        panel.Controls.Add(seeAllButtonInstance)
        panel.Visible = True
    End Sub
    Private Sub OverviewEnrolledCourses()
        Dim courses As List(Of StudentClassCourse) = FetchAllStudentClassCourse(parseId)

        Dim panelWidth = 908
        Dim panelHeight = 174
        Dim spaceBetweenPanels As Integer = 10

        availableCoursesPanel.BackColor = Color.Transparent
        availableCoursesPanel.Size = New Size(1000, 365)
        availableCoursesPanel.Location = New Point(0, 380)
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
            SeeAllButton()
        Else
            NoAvailableEnrooledCoursePanel()
        End If
        Me.Controls.Add(availableCoursesPanel)
    End Sub
    Private Sub NoAvailableEnrooledCoursePanel()
        noAvailableCoursesPanel = Tools.CreatePanel(Me, 970, 364, 0, 380)
        Dim noCourseAvailableInstance As New NoCoursesAvailable
        noAvailableCoursesPanel.Controls.Add(noCourseAvailableInstance)
        noAvailableCoursesPanel.Visible = True

        Dim cover As Label = Tools.CreateButton(Me, 45, 20, 921, 344)
        cover.BackColor = Color.White
    End Sub
    Private Sub SeeAllButton()
        seeAllBtn = Tools.CreateButton(Me, 45, 20, 921, 344)
        AddHandler seeAllBtn.Click, Sub(sender As Object, e As EventArgs)
                                        Handlers.TestButton()
                                    End Sub
    End Sub
End Class
