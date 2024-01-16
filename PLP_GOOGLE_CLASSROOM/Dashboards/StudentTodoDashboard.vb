Public Class StudentTodoDashboard
    Public parseId = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Public availableTodoPanel As New FlowLayoutPanel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentTodoDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateStudentCourse()
        RefreshTodoButton()
    End Sub
    Private Sub CreateStudentCourse()
        Dim courses As List(Of StudentClassCourse) = FetchAllStudentClassCourse(parseId)

        Dim panelWidth = 1019
        Dim panelHeight = 207
        Dim spaceBetweenPanels As Integer = 10

        availableTodoPanel.BackColor = Color.Transparent
        availableTodoPanel.Size = New Size(1200, 365)
        availableTodoPanel.Location = New Point(0, 62)
        availableTodoPanel.FlowDirection = FlowDirection.TopDown
        availableTodoPanel.WrapContents = False
        availableTodoPanel.AutoScroll = True

        If courses.Count > 0 Then
            For Each course In courses
                Dim xCoordinate As Integer = 0
                Dim yCoordinate As Integer = If(availableTodoPanel.Controls.Count > 0, availableTodoPanel.Controls(availableTodoPanel.Controls.Count - 1).Bottom + spaceBetweenPanels, 0)
                Dim coursePanel As Panel = Tools.CreatePanel(availableTodoPanel, panelWidth, panelHeight, xCoordinate, yCoordinate)
                Handlers.PopulateStudentClassCourse(coursePanel, courses, courses.IndexOf(course))
                availableTodoPanel.Controls.Add(coursePanel)
            Next
        Else
            NoAvailableTodoPanel()
        End If
        Me.Controls.Add(availableTodoPanel)
    End Sub
    Private Sub NoAvailableTodoPanel()
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = "There are currently no pending tasks or to-dos on your list."
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub RefreshTodoButton()
        Dim button As Label = Tools.CreateButton(Me, 147, 43, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.RefreshPanel(availableTodoPanel)
                                     CreateStudentCourse()
                                 End Sub
    End Sub
End Class
