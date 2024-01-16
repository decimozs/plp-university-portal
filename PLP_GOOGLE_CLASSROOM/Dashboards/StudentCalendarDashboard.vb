Public Class StudentCalendarDashboard
    Public parseId = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Dim calendarPanel As New FlowLayoutPanel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentCalendarDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateCalendarClass()
        RefreshButton()
    End Sub
    Private Sub CreateCalendarClass()
        Dim assignedTasks As List(Of Calendar) = FetchAssignTask("2200648")

        Dim panelWidth = 489
        Dim panelHeight = 310
        Dim spaceBetweenPanels As Integer = 10

        calendarPanel.BackColor = Color.Transparent
        calendarPanel.Size = New Size(1000, 654)
        calendarPanel.Location = New Point(0, 62)
        calendarPanel.FlowDirection = FlowDirection.LeftToRight
        calendarPanel.WrapContents = True
        calendarPanel.AutoScroll = True

        If assignedTasks.Count > 0 Then
            For Each course In assignedTasks
                Dim xCoordinate As Integer = 0
                Dim yCoordinate As Integer = If(calendarPanel.Controls.Count > 0, calendarPanel.Controls(calendarPanel.Controls.Count - 1).Bottom + spaceBetweenPanels, 0)
                Dim assignedTasksPanel As Panel = Tools.CreatePanel(calendarPanel, panelWidth, panelHeight, xCoordinate, yCoordinate)
                Handlers.PopulateCalendar(assignedTasksPanel, assignedTasks, assignedTasks.IndexOf(course))
                calendarPanel.Controls.Add(assignedTasksPanel)
            Next
        Else
            NoAssignedTask()
        End If
        Me.Controls.Add(calendarPanel)
    End Sub
    Private Sub NoAssignedTask()
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = "There are currently no courses accessible in your account. Kindly request the course code from your professor."
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub RefreshButton()
        Dim button As Label = Tools.CreateUserLabel(Me, 66, 66, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.RefreshPanel(calendarPanel)
                                     CreateCalendarClass()
                                 End Sub
    End Sub
End Class
