Public Class ViewStudentClassCourse
    Public todosPanel As New FlowLayoutPanel
    Public studentsPassedTodoPanel As New FlowLayoutPanel
    Public aboutPanel As New Panel
    Private Sub ViewStudentClassCourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
    End Sub
    Sub New(courseId As String, classProgram As String, className As String, classDescription As String, classCode As String)
        InitializeComponent()
        CreateViewClassCourse(classProgram, className, classDescription, classCode)
        RefreshButton()
        AssignedTodosButton()
        AboutButton()
        HideScrollBar()
        AssignedTodosDashboard()
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
    Private Sub AboutButton()
        Dim button As Label = Tools.CreateUserLabel(Me, 126, 42, 582, 193)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     todosPanel.Visible = False
                                     studentsPassedTodoPanel.Visible = False
                                     aboutPanel.Visible = True
                                 End Sub
    End Sub
    Private Sub RefreshButton()
        Dim button As Label = Tools.CreateButton(Me, 66, 66, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.TestButton()
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
    Private Sub AboutDashboardPanel()
        aboutPanel = Tools.CreatePanel(Me, 1019, 464, 306, 249)
        aboutPanel.BackColor = Color.Transparent
    End Sub
End Class
