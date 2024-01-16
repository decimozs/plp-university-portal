Public Class StudentTeacherDashboard
    Dim parseId As String = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Dim teacherPanel As New FlowLayoutPanel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentTeacherDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateStudentTeacherDashboard()
        RefreshTodoButton()
    End Sub
    Private Sub CreateStudentTeacherDashboard()
        Dim teachers As List(Of Teacher) = FetchStudentTeacher(parseId)

        Dim panelWidth = 1019
        Dim panelHeight = 207
        Dim spaceBetweenPanels As Integer = 10

        teacherPanel.BackColor = Color.Transparent
        teacherPanel.Size = New Size(1100, 365)
        teacherPanel.Location = New Point(0, 62)
        teacherPanel.FlowDirection = FlowDirection.TopDown
        teacherPanel.WrapContents = False
        teacherPanel.AutoScroll = True

        If teachers.Count > 0 Then
            For Each teacher In teachers
                Dim xCoordinate As Integer = 0
                Dim yCoordinate As Integer = If(teacherPanel.Controls.Count > 0, teacherPanel.Controls(teacherPanel.Controls.Count - 1).Bottom + spaceBetweenPanels, 0)
                Dim coursePanel As Panel = Tools.CreatePanel(teacherPanel, panelWidth, panelHeight, xCoordinate, yCoordinate)
                Handlers.PopulateStudentTeachers(coursePanel, teachers, teachers.IndexOf(teacher))
                teacherPanel.Controls.Add(coursePanel)
            Next
        Else
            NoAvailableTodoPanel()
        End If
        Me.Controls.Add(teacherPanel)
    End Sub
    Private Sub NoAvailableTodoPanel()
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = "There are currently teachers on your account, please enroll on a class course first"
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub RefreshTodoButton()
        Dim button As Label = Tools.CreateButton(Me, 147, 43, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.RefreshPanel(teacherPanel)
                                     CreateStudentTeacherDashboard()
                                 End Sub
    End Sub
End Class
