Public Class Form1
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.Form(Me)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CloseButton()
        MinimizedButton()
        StudentButton()
        TeacherButton()
    End Sub
    Private Sub CloseButton()
        Tools.CloseButton(Me, 36, 43, 1552, 2)
    End Sub
    Private Sub MinimizedButton()
        Tools.MinimizedButton(Me, 36, 43, 1512, 2)
    End Sub
    Private Sub StudentButton()
        Dim studentBtn As Panel = Tools.CreatePanel(Me, 270, 138, 539, 502)
        studentBtn.BackgroundImage = Image.FromFile(Handlers.FilePath("student"))
        studentBtn.Visible = True
        Styles.HoverEffect(studentBtn, "hoverstudent")
        AddHandler studentBtn.Click, Sub(sender As Object, e As EventArgs)
                                         Handlers.OpenWindow(Me, StudentLoginForm)
                                     End Sub
    End Sub
    Private Sub TeacherButton()
        Dim teacherBtn As Panel = Tools.CreatePanel(Me, 270, 138, 813, 502)
        teacherBtn.BackgroundImage = Image.FromFile(Handlers.FilePath("teacher"))
        teacherBtn.Visible = True
        Styles.HoverEffect(teacherBtn, "hoverteacher")
        AddHandler teacherBtn.Click, Sub(sender As Object, e As EventArgs)
                                         Handlers.OpenWindow(Me, TeacherLoginForm)
                                     End Sub
    End Sub
End Class
