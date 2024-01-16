Public Class ViewClassCourse
    Public todosPanel As New FlowLayoutPanel
    Public studentsPassedTodoPanel As New FlowLayoutPanel
    Public aboutPanel As New Panel
    Private Sub ViewClassCourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
    End Sub
    Sub New(courseId As String, classProgram As String, className As String, classDescription As String, classCode As String)
        InitializeComponent()
        CreateViewClassCourse(classProgram, className, classDescription, classCode)
        AddTodoButton(courseId)
        AssignedTodosButton()
        StudentsButton()
        AboutButton()
        HideScrollBar()
        AssignedTodosDashboard()
        StudentsPassedTodoPanelDashboard()
        AboutDashboardPanel()
    End Sub
    Private Sub CreateViewClassCourse(classProgram As String, className As String, classDescription As String, classCode As String)
        Dim classProgramLbl As Label = Tools.CreateUserLabel(Me, 416, 70, 0, 0)
        classProgramLbl.Text = classProgram
        classProgramLbl.Font = New Font("Poppins", 30, FontStyle.Bold)

        Dim classNameLbl As Label = Tools.CreateUserLabel(Me, 833, 50, 7, 72)
        classNameLbl.Text = className
        classNameLbl.Font = New Font("Poppins", 20, FontStyle.Regular)

        Dim classCodeLbl As Label = Tools.CreateUserLabel(Me, 833, 50, 7, 124)
        classCodeLbl.Text = $"Class code: {classCode}"
        classCodeLbl.Font = New Font("Poppins", 20, FontStyle.Regular)

        Dim classDescriptionLbl As Label = Tools.CreateUserLabel(Me, 151, 61, 1483, 0)
        classDescriptionLbl.Text = classDescription
        classDescriptionLbl.Font = New Font("Poppins", 20, FontStyle.Bold)
        classDescriptionLbl.TextAlign = ContentAlignment.TopRight
    End Sub
    Private Sub AssignedTodosButton()
        Dim button As Label = Tools.CreateUserLabel(Me, 219, 42, 306, 193)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     todosPanel.Visible = True
                                     studentsPassedTodoPanel.Visible = False
                                     aboutPanel.Visible = False
                                 End Sub
    End Sub
    Private Sub StudentsButton()
        Dim button As Label = Tools.CreateUserLabel(Me, 126, 42, 582, 193)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     todosPanel.Visible = False
                                     studentsPassedTodoPanel.Visible = True
                                     aboutPanel.Visible = False
                                 End Sub
    End Sub
    Private Sub AboutButton()
        Dim button As Label = Tools.CreateUserLabel(Me, 68, 42, 765, 193)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     todosPanel.Visible = False
                                     studentsPassedTodoPanel.Visible = False
                                     aboutPanel.Visible = True
                                 End Sub
    End Sub
    Private Sub AddTodoButton(courseId As String)
        Dim button As Label = Tools.CreateButton(Me, 66, 66, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.createTodoPanelPopup.Visible = True
                                     TeacherDashboard.createTodoPanelPopup.BringToFront()
                                 End Sub
    End Sub
    Private Sub HideScrollBar()
        Dim hideScrollBarLbl As Label = Tools.CreateUserLabel(Me, 30, 464, 1330, 249)
        hideScrollBarLbl.BackColor = Color.White
    End Sub
    Private Sub NoAvailable(text As String)
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = text
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub AssignedTodosDashboard()
        Dim todos As List(Of Todo) = FetchAllTodo("2200648")

        Dim panelWidth = 1019
        Dim panelHeight = 123
        Dim spaceBetweenPanels As Integer = 10

        todosPanel.BackColor = Color.Transparent
        todosPanel.Size = New Size(1050, 464)
        todosPanel.Location = New Point(303, 249)
        todosPanel.FlowDirection = FlowDirection.TopDown
        todosPanel.WrapContents = False
        todosPanel.AutoScroll = True

        If todos.Count > 0 Then
            For Each todo In todos
                Dim todoClassPanel As Panel = Tools.CreatePanel(todosPanel, panelWidth, panelHeight, 0, 0)
                Handlers.PopulateTodo(todoClassPanel, todos, todos.IndexOf(todo))
                todosPanel.Controls.Add(todoClassPanel)
            Next
        Else
            NoAvailable("Text")
        End If

        Me.Controls.Add(todosPanel)
    End Sub
    Private Sub StudentsPassedTodoPanelDashboard()
        Dim todos As List(Of Todo) = FetchPassedTodo("2200638")

        Dim panelWidth = 1019
        Dim panelHeight = 123
        Dim spaceBetweenPanels As Integer = 10

        studentsPassedTodoPanel.BackColor = Color.Transparent
        studentsPassedTodoPanel.Size = New Size(1050, 464)
        studentsPassedTodoPanel.Location = New Point(303, 249)
        studentsPassedTodoPanel.FlowDirection = FlowDirection.TopDown
        studentsPassedTodoPanel.WrapContents = False
        studentsPassedTodoPanel.AutoScroll = True

        If todos.Count > 0 Then
            For Each todo In todos
                Dim todoClassPanel As Panel = Tools.CreatePanel(studentsPassedTodoPanel, panelWidth, panelHeight, 0, 0)
                Handlers.PopulatePassedTodo(todoClassPanel, todos, todos.IndexOf(todo))
                studentsPassedTodoPanel.Controls.Add(todoClassPanel)
            Next
        Else
            NoAvailable("Text")
        End If

        Me.Controls.Add(studentsPassedTodoPanel)
    End Sub
    Private Sub AboutDashboardPanel()
        aboutPanel = Tools.CreatePanel(Me, 1019, 464, 306, 249)
        aboutPanel.BackColor = Color.Transparent
    End Sub
End Class
