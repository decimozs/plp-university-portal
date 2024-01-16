Imports iText.Kernel.Pdf.Colorspace.PdfDeviceCs
Imports iText.StyledXmlParser.Jsoup.Select.Evaluator

Public Class TeacherDashboard
    Public parseId = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Public createCoursePanelPopup As New Panel
    Public createAnnouncementPanelPopup As New Panel
    Public createTodoPanelPopup As New Panel
    Public createDiscussionForumPanelPopup As New Panel
    Public noAvailableCoursePanel As New Panel
    Public noAvaialbleAnnouncementPanel As New Panel
    Public avaialbleAnnouncementPanel As New FlowLayoutPanel
    Public popUpPanel As New Panel
    Public menuPanel As New Panel
    Public classesPanel As New Panel
    Public todoPanel As New Panel
    Public gradesPanel As New Panel
    Public calendarPanel As New Panel
    Public discussionForumPanel As New Panel
    Public announcementPanel As New Panel
    Public helpPanel As New Panel
    Public classCourse As New Panel
    Public availableCoursesPanel As New FlowLayoutPanel
    Public viewAnnouncementPanel As New Panel
    Public dashboardPanel As New Panel
    Public gradedViewPanel As New Panel
    Public listTodoPanel As New Panel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub TeacherDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.Dashboard(Me)
        LoadComponents()
    End Sub
    Public Sub LoadComponents()
        CreateAnnnouncementPanel()
        CreateDiscussionForumPanel()
        CreateTodoPanel()
        DashboardOverviewPanel()
        TeacherName()
        TeacherPositon()
        MenuButton()
        Menu()
        PopUp()
        CreateCoursePanel()
        CloseButton()
        MinimizedButton()
    End Sub
    Private Sub CloseButton()
        Tools.CloseButton(Me, 36, 43, 1749, 2)
    End Sub
    Private Sub MinimizedButton()
        Tools.MinimizedButton(Me, 36, 43, 1704, 2)
    End Sub
    Public Sub TeacherName()
        Dim teacherNameLbl = Tools.CreateUserLabel(Me, 926, 25, 233, 85)
        teacherNameLbl.text = Handlers.GetTeacherName(parseId)
        teacherNameLbl.Font = New Font("Poppins", 9, FontStyle.Bold)
    End Sub
    Public Sub TeacherPositon()
        Dim teacherPositionLbl As Label = Tools.CreateUserLabel(Me, 500, 25, 233, 105)
        teacherPositionLbl.ForeColor = ColorTranslator.FromHtml("#9C8B8B")
        teacherPositionLbl.Text = Handlers.GetTeacherPosition(parseId)
        teacherPositionLbl.Font = New Font("Poppins", 9, FontStyle.Bold)
    End Sub
    Private Sub CreateAnnnouncementPanel()
        createAnnouncementPanelPopup = Tools.CreatePanel(Me, 676, 777, 1053, 177)
        Dim createAnnouncementInstance As New CreateAnnouncement
        createAnnouncementPanelPopup.Controls.Add(createAnnouncementInstance)
    End Sub
    Private Sub CreateDiscussionForumPanel()
        createDiscussionForumPanelPopup = Tools.CreatePanel(Me, 676, 777, 1053, 177)
        Dim createDiscussionInstance As New CreateDiscussionForum
        createDiscussionForumPanelPopup.Controls.Add(createDiscussionInstance)
    End Sub
    Private Sub CreateCoursePanel()
        createCoursePanelPopup = Tools.CreatePanel(Me, 676, 636, 574, 248)
        Dim createClassInstance As New CreateClass
        createCoursePanelPopup.Controls.Add(createClassInstance)
    End Sub
    Private Sub CreateTodoPanel()
        createTodoPanelPopup = Tools.CreatePanel(Me, 676, 963, 1053, 20)
        Dim createTodoInstance As New CreateTodo
        createTodoPanelPopup.Controls.Add(createTodoInstance)
    End Sub
    Public Sub ViewAnnouncementPopup(id As String, title As String, description As String, teacherNo As String)
        viewAnnouncementPanel = Tools.CreatePanel(Me, 1019, 369, 402, 299)
        viewAnnouncementPanel.BringToFront()
        Dim viewAnnouncementInstance As New ViewAnnouncement(id, title, description, teacherNo)
        viewAnnouncementPanel.Controls.Add(viewAnnouncementInstance)
        viewAnnouncementPanel.Visible = True
    End Sub
    Public Sub PopUp()
        popUpPanel = Tools.CreatePanel(Me, 407, 87, 708, 53)
        Dim errorHandlersInstance As New ErrorHandlers
        errorHandlersInstance.errorHandlersLabel.Text = "Successfully created a class"
        popUpPanel.Controls.Add(errorHandlersInstance)
        popUpPanel.BringToFront()
    End Sub
    Private Sub Menu()
        menuPanel = Tools.CreatePanel(Me, 389, 962, 0, 44)
        Dim menuPanelInstance As New TeacherMenu
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
    Public Sub ViewClassCourse(id As String, program As String, name As String, description As String, classCode As String)
        classCourse = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        classCourse.BringToFront()
        Dim viewClassCourseInstance As New ViewClassCourse(id, program, name, description, classCode)
        classCourse.Controls.Add(viewClassCourseInstance)
        classCourse.Visible = True
    End Sub
    Public Sub ViewGradedTodo(id As String, title As String, studentName As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, grade As String, teacherNo As String)
        gradedViewPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        gradedViewPanel.BringToFront()
        Dim viewGradedInstance As New TeacherGradedView(id, title, studentName, deadline, filename, filepath, createdAt, deadline, grade, teacherNo)
        gradedViewPanel.Controls.Add(viewGradedInstance)
        gradedViewPanel.Visible = True
    End Sub
    'test
    Public Sub ViewListTodo(id As String, title As String, description As String, filename As String, filepath As String, createdAt As String, deadline As String, teacherNo As String, studentName As String, grade As String)
        listTodoPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        listTodoPanel.BringToFront()
        Dim viewListTodoInstance As New ViewListTodo(id, title, description, filename, filepath, createdAt, deadline, teacherNo, studentName, grade)
        listTodoPanel.Controls.Add(viewListTodoInstance)
        listTodoPanel.Visible = True
    End Sub

    Public Sub DashboardOverviewPanel()
        dashboardPanel = Tools.CreatePanel(Me, 1631, 774, 98, 182)
        dashboardPanel.BringToFront()
        dashboardPanel.BackColor = Color.Transparent
        Dim dashboardInstance As New TeacherOverviewDashboard
        dashboardPanel.Controls.Add(dashboardInstance)
        dashboardPanel.Visible = True
    End Sub
    Public Sub ClassDashboardPanel()
        classesPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        classesPanel.BringToFront()
        Dim teacherClassDashboardInstance As New TeacherClassDashboard
        classesPanel.Controls.Add(teacherClassDashboardInstance)
        classesPanel.Visible = True
    End Sub
    Public Sub TodoDashboardPanel()
        todoPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        todoPanel.BringToFront()
        Dim teacherTodoDashboardInstance As New TeacherTodoDashboard
        todoPanel.Controls.Add(teacherTodoDashboardInstance)
        todoPanel.Visible = True
    End Sub
    Public Sub CalendarDashboardPanel()
        calendarPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        calendarPanel.BringToFront()
        Dim calendarDashboardInstance As New TeacherCalendarDashboard
        calendarPanel.Controls.Add(calendarDashboardInstance)
        calendarPanel.Visible = True
    End Sub
    Public Sub DiscussionForumDashboardPanel()
        discussionForumPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        discussionForumPanel.BringToFront()
        Dim discussionDashboardInstance As New DiscussionForumDashboard
        discussionForumPanel.Controls.Add(discussionDashboardInstance)
        discussionForumPanel.Visible = True
    End Sub
    Public Sub AnnouncementDashboardPanel()
        announcementPanel = Tools.CreatePanel(Me, 1631, 748, 98, 182)
        announcementPanel.BringToFront()
        announcementPanel.BackColor = Color.Transparent
        Dim annoucementDashboardInstance As New AnnouncementDashboard
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
