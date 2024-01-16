Public Class StudentCourseDashboard
    Public parseId = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Public availableCoursesPanel As New FlowLayoutPanel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentCourseDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateStudentCourse()
        JoinClassButton()
    End Sub
    Private Sub CreateStudentCourse()
        Dim courses As List(Of StudentClassCourse) = FetchAllStudentClassCourse(parseId)

        Dim panelWidth = 908
        Dim panelHeight = 174
        Dim spaceBetweenPanels As Integer = 10

        availableCoursesPanel.BackColor = Color.Transparent
        availableCoursesPanel.Size = New Size(1000, 654)
        availableCoursesPanel.Location = New Point(0, 62)
        availableCoursesPanel.FlowDirection = FlowDirection.TopDown
        availableCoursesPanel.WrapContents = False
        availableCoursesPanel.AutoScroll = True

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
    Private Sub NoAvailableEnrooledCoursePanel()
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = "There are currently no courses accessible in your account. Kindly request the course code from your professor."
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub JoinClassButton()
        Dim button As Label = Tools.CreateButton(Me, 147, 43, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     StudentDashboard.joinClassPopup.Visible = True
                                     StudentDashboard.joinClassPopup.BringToFront()
                                 End Sub
    End Sub
End Class
