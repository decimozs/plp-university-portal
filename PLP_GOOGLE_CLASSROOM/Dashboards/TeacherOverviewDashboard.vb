Public Class TeacherOverviewDashboard
    Public parseId = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Public avaialbleAnnouncementPanel As New FlowLayoutPanel
    Public availableCoursesPanel As New FlowLayoutPanel
    Public noAvailableCoursePanel As New Panel
    Public noAvaialbleAnnouncementPanel As New Panel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub OverviewDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1640, 774)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateOverviewDashboard()
        OverViewAnnouncementPanel()
        OverviewClassPanel()
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
        teacherDepartmentLbl.Text = Handlers.GetTeacherDepartment(parseId)
        teacherDepartmentLbl.Font = New Font("Poppins", 30, FontStyle.Bold)
        teacherDepartmentLbl.TextAlign = ContentAlignment.MiddleRight

        Dim greetingsLbl As Label = Tools.CreateUserLabel(Me, 767, 70, 828, 174)
        greetingsLbl.ForeColor = Color.White
        greetingsLbl.Text = $"Welcome Back, {Handlers.GetTeacherFirstName(parseId)}!"
        greetingsLbl.Font = New Font("Poppins", 20, FontStyle.Bold)
        greetingsLbl.TextAlign = ContentAlignment.MiddleRight

        Dim text As Label = Tools.CreateUserLabel(Me, 767, 30, 820, 234)
        text.ForeColor = Color.White
        text.Text = "Always stay updated in your teacher portal"
        text.Font = New Font("Poppins", 11, FontStyle.Regular)
        text.TextAlign = ContentAlignment.MiddleRight
    End Sub
    Private Sub NoAvailableClassPanel()
        noAvailableCoursePanel = Tools.CreatePanel(Me, 970, 364, 0, 380)
        Dim noClassAvailableInstance As New NoClassAvailable
        noAvailableCoursePanel.Controls.Add(noClassAvailableInstance)
        noAvailableCoursePanel.Visible = True
    End Sub
    Private Sub NoAvailableAnnoncementPanel()
        noAvaialbleAnnouncementPanel = Tools.CreatePanel(Me, 621, 364, 1010, 377)
        Dim noAnnouncementAvailableInstance As New NoAnnouncementAvailable
        noAvaialbleAnnouncementPanel.Controls.Add(noAnnouncementAvailableInstance)
        noAvaialbleAnnouncementPanel.Visible = True
    End Sub
    Public Sub OverViewAnnouncementPanel()
        Dim announcements As List(Of Announcement) = FetchAvailableAnnouncement()

        Dim panelWidth = 621
        Dim panelHeight = 125
        Dim spaceBetweenPanels As Integer = 10

        avaialbleAnnouncementPanel.BackColor = Color.Transparent
        avaialbleAnnouncementPanel.Size = New Size(625, 275)
        avaialbleAnnouncementPanel.Location = New Point(1003, 379)
        avaialbleAnnouncementPanel.FlowDirection = FlowDirection.TopDown
        avaialbleAnnouncementPanel.WrapContents = False

        If announcements.Count > 0 Then
            For Each announcement In announcements
                Dim announcementClassPanel As Panel = Tools.CreatePanel(avaialbleAnnouncementPanel, panelWidth, panelHeight, 0, 0)
                Handlers.PopulateAvailableAnnouncement(announcementClassPanel, announcements, announcements.IndexOf(announcement))
                avaialbleAnnouncementPanel.Controls.Add(announcementClassPanel)
            Next
            SeeAllAnnouncmentButton()
        Else
            NoAvailableAnnoncementPanel()
        End If

        Me.Controls.Add(avaialbleAnnouncementPanel)
    End Sub
    Private Sub SeeAllAnnouncmentButton()
        Dim panel As Panel = Tools.CreatePanel(Me, 621, 78, 1005, 658)
        Dim seeAllButtonInstance As New SeeAllButton()
        panel.Controls.Add(seeAllButtonInstance)
        panel.Visible = True
    End Sub
    Public Sub OverviewClassPanel()
        Dim courses As List(Of [Class]) = FetchTeacherClass(parseId)

        Dim panelWidth As Integer = 439
        Dim panelHeight As Integer = 174

        availableCoursesPanel.BackColor = Color.Transparent
        availableCoursesPanel.Size = New Size(898, 365)
        availableCoursesPanel.Location = New Point(0, 380)
        availableCoursesPanel.FlowDirection = FlowDirection.LeftToRight
        availableCoursesPanel.WrapContents = True

        If courses.Count > 0 Then
            For Each course In courses
                Dim xCoordinate As Integer = If(availableCoursesPanel.Controls.Count Mod 2 = 0, 98, 557)
                Dim yCoordinate As Integer = If(availableCoursesPanel.Controls.Count < 2, 566, 756)
                Dim classPanel As Panel = Tools.CreatePanel(availableCoursesPanel, panelWidth, panelHeight, xCoordinate, yCoordinate)
                Handlers.PopulateClassPanel(classPanel, courses, courses.IndexOf(course))
                availableCoursesPanel.Controls.SetChildIndex(classPanel, availableCoursesPanel.Controls.Count - 1)
            Next
        Else
            NoAvailableClassPanel()
        End If

        Me.Controls.Add(availableCoursesPanel)
    End Sub
End Class
