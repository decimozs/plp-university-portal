Public Class TeacherClassDashboard
    Public parseId = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Public availableClassesPanel As New FlowLayoutPanel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub TeacherClassDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
        LoadComponents()
    End Sub
    Public Sub LoadComponents()
        CreateTeacherClass()
        CreateClassButton()
    End Sub
    Public Sub CreateTeacherClass()
        Dim courses As List(Of [Class]) = FetchAllTeacherClass(parseId)

        Dim panelWidth As Integer = 439
        Dim panelHeight As Integer = 174

        availableClassesPanel.BackColor = Color.Transparent
        availableClassesPanel.Size = New Size(1389, 654)
        availableClassesPanel.Location = New Point(0, 62)
        availableClassesPanel.FlowDirection = FlowDirection.LeftToRight
        availableClassesPanel.WrapContents = True

        If courses.Count > 0 Then
            For Each course In courses
                Dim xCoordinate As Integer = If(availableClassesPanel.Controls.Count Mod 2 = 0, 98, 557)
                Dim yCoordinate As Integer = If(availableClassesPanel.Controls.Count < 2, 566, 756)
                Dim classPanel As Panel = Tools.CreatePanel(availableClassesPanel, panelWidth, panelHeight, xCoordinate, yCoordinate)
                Handlers.PopulateClassPanel(classPanel, courses, courses.IndexOf(course))
                availableClassesPanel.Controls.SetChildIndex(classPanel, availableClassesPanel.Controls.Count - 1)
            Next
        Else
            NoClassAvailable()
        End If

        Me.Controls.Add(availableClassesPanel)
    End Sub
    Private Sub NoClassAvailable()
        Dim label As Label = Tools.CreateUserLabel(Me, 871, 34, 380, 340)
        label.Text = "There are currently no created courses accessible in your account."
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub CreateClassButton()
        Dim button As Label = Tools.CreateButton(Me, 66, 66, 1556, 670)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.createCoursePanelPopup.Visible = True
                                     TeacherDashboard.createCoursePanelPopup.BringToFront()
                                 End Sub
    End Sub
End Class