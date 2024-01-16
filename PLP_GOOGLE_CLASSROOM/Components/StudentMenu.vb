
Public Class StudentMenu
    Private Sub Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponent()
        Styles.UserControl(Me, 389, 962)
    End Sub
    Sub New()
        InitializeComponent()
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateMenu()
        LogoutButton()
    End Sub
    Private Sub CreateMenu()
        CreateMenuButton("Dashboard", 49)
        CreateMenuButton("Courses", 138)
        CreateMenuButton("To-do", 227)
        CreateMenuButton("Grades", 316)
        CreateMenuButton("Calendar", 405)
        CreateMenuButton("Teacher", 493)
        CreateMenuButton("Discussion Forum", 584)
        CreateMenuButton("Announcements", 673)
        CreateMenuButton("Help", 753)
        LogoutButton()
    End Sub
    Private Sub CreateMenuButton(buttonName As String, y As Integer)
        Dim button As Label = Tools.CreateButton(Me, 389, 60, 3, y)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     StudentDashboard.menuPanel.Visible = False
                                     ResetPanelsVisibility(StudentDashboard)

                                     Select Case buttonName
                                         Case "Dashboard"
                                             StudentDashboard.DashboardOverviewPanel()
                                         Case "Courses"
                                             StudentDashboard.CoursesDashboardPanel()
                                         Case "To-do"
                                             StudentDashboard.TodoDashboardPanel()
                                         Case "Grades"
                                             StudentDashboard.GradesDashboardPanel()
                                         Case "Calendar"
                                             StudentDashboard.CalendarDashboardPanel()
                                         Case "Teacher"
                                             StudentDashboard.TeacherDashboardPanel()
                                         Case "Discussion Forum"
                                             StudentDashboard.DiscussionForumDashboardPanel()
                                         Case "Announcements"
                                             StudentDashboard.AnnouncementDashboardPanel()
                                         Case "Help"
                                             StudentDashboard.HelpDashboardPanel()
                                     End Select
                                 End Sub
    End Sub
    Private Sub ResetPanelsVisibility(dashboard As StudentDashboard)
        dashboard.coursesPanel.Visible = False
        dashboard.todoPanel.Visible = False
        dashboard.gradesPanel.Visible = False
        dashboard.calendarPanel.Visible = False
        dashboard.discussionForumPanel.Visible = False
        dashboard.announcementPanel.Visible = False
        dashboard.helpPanel.Visible = False
    End Sub
    Private Sub LogoutButton()
        Dim logoutBtn As Panel = Tools.CreatePanel(Me, 291, 81, 49, 837)
        logoutBtn.Visible = True
        logoutBtn.BackgroundImage = Image.FromFile(Handlers.FilePath("logout"))
        Styles.HoverEffect(logoutBtn, "hoverlogout")
        AddHandler logoutBtn.Click, Sub(sender As Object, e As EventArgs)
                                        StudentDashboard.menuPanel.Visible = False
                                        StudentLoginForm.ResetTextBox()
                                        Handlers.DisposeWindow(StudentDashboard, Form1)
                                    End Sub
    End Sub
End Class
