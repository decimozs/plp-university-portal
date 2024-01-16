Imports System.Drawing.Design
Public Class TeacherClass
    Public tableName As String = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Dim deletePanel As New Panel
    Private Sub ClassComponent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 439, 174)
        InitializeComponent()
    End Sub
    Sub New(courseID As String, coureProgram As String, courseName As String, courseDescription As String, type As String, classCode As String)
        InitializeComponent()
        CreateClass(coureProgram, courseName, courseDescription)
        ModifyButton()
        DeleteButton(courseID, type)
        ViewButton(courseID, coureProgram, courseName, courseDescription, classCode)
    End Sub
    Private Sub CreateClass(coureProgram As String, courseName As String, courseDescription As String)
        Dim courseProgramLbl As Label = Tools.CreateUserLabel(Me, 200, 40, 30, 21)
        courseProgramLbl.Text = coureProgram
        courseProgramLbl.Font = New Font("Poppins", 15, FontStyle.Bold)

        Dim courseNameLbl As Label = Tools.CreateUserLabel(Me, 367, 50, 34, 58)
        courseNameLbl.Text = courseName
        courseNameLbl.Font = New Font("Poppins", 10, FontStyle.Regular)

        Dim courseDescriptionLbl As Label = Tools.CreateUserLabel(Me, 100, 50, 34, 120)
        courseDescriptionLbl.Text = courseDescription
        courseDescriptionLbl.Font = New Font("Poppins", 10, FontStyle.Regular)
    End Sub
    Private Sub ModifyButton()
        Dim button As Label = Tools.CreateButton(Me, 7, 25, 405, 20)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.OpenPanel(deletePanel)
                                 End Sub
    End Sub
    Private Sub DeleteButton(courseID As String, type As String)
        deletePanel = Tools.CreatePanel(Me, 107, 34, 293, 16)
        Dim button As Label = Tools.CreateButton(Me, 95, 23, 5, 6)
        button.BackColor = ColorTranslator.FromHtml("#FF4141")
        button.Text = "Delete class"
        button.ForeColor = Color.White
        button.Font = New Font("Poppins", 8, FontStyle.Bold)
        button.TextAlign = ContentAlignment.MiddleCenter
        button.BringToFront()
        Dim deleteInstance As New DeleteClass
        deletePanel.Controls.Add(button)
        deletePanel.Controls.Add(deleteInstance)
        deletePanel.BringToFront()
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     If type = "overview" Then
                                         Handlers.DeleteClassCourse(tableName, courseID)
                                         Handlers.DeleteStudentClassCourse("2200638", courseID)
                                     ElseIf type = "" Then
                                         Handlers.DeleteClassCourse(tableName, courseID)
                                         Handlers.DeleteStudentClassCourse("2200638", courseID)
                                     End If
                                 End Sub
    End Sub
    Private Sub ViewButton(courseID As String, coureProgram As String, courseName As String, courseDescription As String, classCode As String)
        Dim button As Label = Tools.CreateButton(Me, 117, 37, 299, 117)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.ViewClassCourse(courseID, coureProgram, courseName, courseDescription, classCode)
                                 End Sub
    End Sub
End Class
