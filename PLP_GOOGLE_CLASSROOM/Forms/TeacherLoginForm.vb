Public Class TeacherLoginForm
    Public teacherId As New TextBox
    Private password As New TextBox
    Private loginBtn As New Panel
    Public errorHandlersPanel As New Panel
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub TeacherLoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.Form(Me)
        LoadComponents()
        Me.BeginInvoke(New Action(Sub() Me.ActiveControl = Nothing))
    End Sub
    Private Sub LoadComponents()
        StudentIdTextBox()
        PasswordIdTextBox()
        LoginButton()
        CloseButton()
        MinimizedButton()
        ErrorHandlers()
        BackButton()
    End Sub
    Private Sub BackButton()
        Tools.BackButton(Me, Form1, 75, 29, 75, 959)
    End Sub
    Private Sub CloseButton()
        Tools.CloseButton(Me, 36, 43, 1552, 2)
    End Sub
    Private Sub MinimizedButton()
        Tools.MinimizedButton(Me, 36, 43, 1512, 2)
    End Sub
    Private Sub StudentIdTextBox()
        teacherId = Tools.CreateTextBox(Me, "Enter your teacher id", 450, 38, 590, 488)
    End Sub
    Private Sub PasswordIdTextBox()
        password = Tools.CreateTextBox(Me, "Enter your password", 450, 38, 590, 570)
    End Sub
    Private Sub LoginButton()
        loginBtn = Tools.CreatePanel(Me, 249, 58, 685, 645)
        loginBtn.BackgroundImage = Image.FromFile(Handlers.FilePath("login"))
        loginBtn.Visible = True
        Styles.HoverEffect(loginBtn, "hoverlogin")
        AddHandler loginBtn.Click, Sub(sender As Object, e As EventArgs)
                                       Handlers.TeacherLoginAccess(teacherId.Text, password.Text)
                                       Handlers.CreateTeacherResources(teacherId.Text)
                                   End Sub
    End Sub
    Public Sub ErrorHandlers()
        errorHandlersPanel = Tools.CreatePanel(Me, 407, 87, 607, 53)
        Dim errorHandlersInstance As New ErrorHandlers
        errorHandlersInstance.errorHandlersLabel.Text = "Invalid Credentials"
        errorHandlersPanel.Controls.Add(errorHandlersInstance)
    End Sub
    Public Sub ResetTextBox()
        teacherId.Text = "Enter your student id"
        password.Text = "Enter your password"
    End Sub
End Class