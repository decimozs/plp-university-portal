Public Class TeacherTodoDashboard
    Public parseId = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Public availableTodoPanel As New FlowLayoutPanel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub TeacherTodoDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateTodoDashboard()
        RefreshButton()
    End Sub
    Private Sub CreateTodoDashboard()
        Dim todos As List(Of Todo) = FetchAllTodo("2200648")

        Dim panelWidth = 1019
        Dim panelHeight = 170
        Dim spaceBetweenPanels As Integer = 10

        availableTodoPanel.BackColor = Color.Transparent
        availableTodoPanel.Size = New Size(1100, 654)
        availableTodoPanel.Location = New Point(0, 62)
        availableTodoPanel.FlowDirection = FlowDirection.TopDown
        availableTodoPanel.WrapContents = False
        availableTodoPanel.AutoScroll = True

        If todos.Count > 0 Then
            For Each todo In todos
                Dim todoClassPanel As Panel = Tools.CreatePanel(availableTodoPanel, panelWidth, panelHeight, 0, 0)
                Handlers.PopulateListTodo(todoClassPanel, todos, todos.IndexOf(todo))
                availableTodoPanel.Controls.Add(todoClassPanel)
            Next
        Else
            NoAvailableTodo()
        End If

        Me.Controls.Add(availableTodoPanel)
    End Sub
    Private Sub NoAvailableTodo()
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = "There are currently no courses accessible in your account. Kindly request the course code from your professor."
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub HideScrollBar()

    End Sub
    Private Sub RefreshButton()
        Dim button As Label = Tools.CreateButton(Me, 147, 43, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     StudentDashboard.joinClassPopup.Visible = True
                                     StudentDashboard.joinClassPopup.BringToFront()
                                 End Sub
    End Sub
End Class
