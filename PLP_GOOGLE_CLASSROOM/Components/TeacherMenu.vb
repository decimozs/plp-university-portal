Public Class TeacherMenu
    Private Sub TeacherMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponent()
        Styles.UserControl(Me, 389, 962)
    End Sub
    Sub New()
        InitializeComponent()
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateMenu()
    End Sub
    Private Sub CreateMenu()
        CreateMenuButton("Dashboard", 49)
        CreateMenuButton("Courses", 138)
        CreateMenuButton("To-do", 227)
        CreateMenuButton("Calendar", 315)
        CreateMenuButton("Discussion Forum", 405)
        CreateMenuButton("Announcements", 486)
        CreateMenuButton("Help", 757)
        LogoutButton()
    End Sub
    Private Sub CreateMenuButton(buttonName As String, y As Integer)
        Dim button As Label = Tools.CreateButton(Me, 389, 60, 0, y)

        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.menuPanel.Visible = False
                                     ResetPanelsVisibility(TeacherDashboard)

                                     Select Case buttonName
                                         Case "Dashboard"
                                             TeacherDashboard.DashboardOverviewPanel()
                                         Case "Courses"
                                             TeacherDashboard.ClassDashboardPanel()
                                         Case "To-do"
                                             TeacherDashboard.TodoDashboardPanel()
                                         Case "Calendar"
                                             TeacherDashboard.CalendarDashboardPanel()
                                         Case "Discussion Forum"
                                             TeacherDashboard.DiscussionForumDashboardPanel()
                                         Case "Announcements"
                                             TeacherDashboard.AnnouncementDashboardPanel()
                                         Case "Help"
                                             TeacherDashboard.HelpDashboardPanel()
                                     End Select
                                 End Sub
    End Sub

    Private Sub ResetPanelsVisibility(dashboard As TeacherDashboard)
        dashboard.classesPanel.Visible = False
        dashboard.todoPanel.Visible = False
        dashboard.gradesPanel.Visible = False
        dashboard.calendarPanel.Visible = False
        dashboard.discussionForumPanel.Visible = False
        dashboard.announcementPanel.Visible = False
        dashboard.helpPanel.Visible = False
        dashboard.classCourse.Visible = False
    End Sub

    Private Sub LogoutButton()
        Dim logoutBtn As Panel = Tools.CreatePanel(Me, 291, 81, 49, 837)
        logoutBtn.Visible = True
        logoutBtn.BackgroundImage = Image.FromFile(Handlers.FilePath("logout"))
        Styles.HoverEffect(logoutBtn, "hoverlogout")
        AddHandler logoutBtn.Click, Sub(sender As Object, e As EventArgs)
                                        TeacherDashboard.menuPanel.Visible = False
                                        TeacherLoginForm.ResetTextBox()
                                        Handlers.DisposeWindow(TeacherDashboard, Form1)
                                    End Sub
    End Sub
End Class
